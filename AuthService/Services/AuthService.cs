using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AuthService.Data;
using AuthService.Dtos;
using AuthService.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Services
{
    public interface IAuthenService
    {
        AccessTokenResponseDto GenerateToken(string username, string pwd);
        AccountReadDto Register(RegisterDto dto);
    }

    public class AuthenService : IAuthenService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthenService(IUnitOfWork uow, IMapper mapper, IConfiguration configuration)
        {
            _uow = uow;
            _mapper = mapper;
            _configuration = configuration;
        }
        public AccessTokenResponseDto GenerateToken(string username, string pwd)
        {
            var account = _uow.AccountRepo.Find(_ => _.Username == username).Include(_ => _.Role).FirstOrDefault();
            if (account != null)
            {
                if (Check(account.Hashed, pwd))
                {
                    var claims = new[]
                        {
                        new Claim(JwtRegisteredClaimNames.Sub, account.Id.ToString(), ClaimValueTypes.Integer),
                        new Claim(ClaimTypes.Role, account.Role.Name),
                        new Claim(ClaimTypes.Name, account.Id.ToString(), ClaimValueTypes.Integer),
                        new Claim(ClaimTypes.Email, account.Email),
                        new Claim(ClaimTypes.MobilePhone, account.Phone),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iss, _configuration["Jwt:Iss"])
                    };
                    var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(_configuration["Jwt:Iss"], null, claims, null, DateTime.Now.AddDays(7), creds);
                    account.FailedAccessTimes = 0;
                    account.LastAccessed = DateTime.UtcNow;
                    _uow.Complete();
                    return new AccessTokenResponseDto
                    {
                        AccountId = account.Id,
                        AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
                    };
                }
                else
                {
                    account.FailedAccessTimes += 1;
                    _uow.Complete();
                }
            }
            return null;
        }

        public AccountReadDto Register(RegisterDto dto)
        {
            var account = new Account
            {
                Username = dto.Username,
                Email = dto.Email,
                RoleId = dto.RoleId,
                Hashed = Hash(dto.Password),
                CreatedTime = DateTime.UtcNow,
                ModifiedTime = DateTime.UtcNow,
                Phone = dto.Phone
            };
            _uow.AccountRepo.Add(account);
            if (_uow.Complete() > 0)
            {
                account = _uow.AccountRepo.Find(_ => _.Username == dto.Username).Include(_ => _.Role).FirstOrDefault();
                return _mapper.Map<AccountReadDto>(account);
            }
            return null;
        }

        #region private 

        private string Hash(string password)
        {
            var iters = (new Random()).Next(1000, 5000);
            using (var algorithm = new Rfc2898DeriveBytes(
              password,
              16,
              iters,
              HashAlgorithmName.SHA256))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(32));
                var salt = Convert.ToBase64String(algorithm.Salt);
                return $"{iters}.{salt}.{key}";
            }
        }

        private bool Check(string hashed, string pwd)
        {
            var parts = hashed.Split('.', 3);

            if (parts.Length != 3)
            {
                throw new FormatException("--> Unexpected hash format. " +
                  "Should be formatted as `{iterations}.{salt}.{hash}`");
            }
            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);
            using (var algorithm = new Rfc2898DeriveBytes(pwd, salt, iterations, HashAlgorithmName.SHA256))
            {
                var keyToCheck = algorithm.GetBytes(32);
                var verified = keyToCheck.SequenceEqual(key);
                return verified;
            }
        }

        #endregion
    }
}