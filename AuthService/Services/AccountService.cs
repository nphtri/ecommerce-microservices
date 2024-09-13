using System.Collections.Generic;
using AuthService.Data;
using AuthService.Dtos;
using AutoMapper;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Services
{
    public interface IAccountService
    {
        IEnumerable<AccountReadDto> GetAccounts();
        AccountReadDto GetAccountById(int id);
        AccountReadDto GetAccountByUserName(string username);
        AccountReadDto GetAccountByEmail(string email);
        AccountReadDto GetAccountByPhone(string phone);
        IEnumerable<RoleReadDto> GetRoles();
    }
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AccountService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public AccountReadDto GetAccountByUserName(string username)
        {
            var result = _uow.AccountRepo.Find(_ => _.Username == username).Include(_ => _.Role).FirstOrDefault();
            if (result != null)
            {
                return _mapper.Map<AccountReadDto>(result);
            }
            return null;
        }
        public AccountReadDto GetAccountByPhone(string phone)
        {
            var result = _uow.AccountRepo.Find(_ => _.Phone == phone).Include(_ => _.Role).FirstOrDefault();
            if (result != null)
            {
                return _mapper.Map<AccountReadDto>(result);
            }
            return null;
        }

        public AccountReadDto GetAccountByEmail(string email)
        {
            var result = _uow.AccountRepo.Find(_ => _.Email == email).Include(_ => _.Role).FirstOrDefault();
            if (result != null)
            {
                return _mapper.Map<AccountReadDto>(result);
            }
            return null;
        }

        public IEnumerable<AccountReadDto> GetAccounts()
        {
            var result = _uow.AccountRepo.GetAll().Include(_ => _.Role).AsEnumerable();
            return _mapper.Map<IEnumerable<AccountReadDto>>(result);
        }
        public IEnumerable<RoleReadDto> GetRoles() {
            return _mapper.Map<IEnumerable<RoleReadDto>>(_uow.RoleRepo.GetAll().AsEnumerable());
        }

        public AccountReadDto GetAccountById(int id)
        {
            var result = _uow.AccountRepo.Find(_ => _.Id == id).Include(_ => _.Role).FirstOrDefault();
            if (result != null)
            {
                return _mapper.Map<AccountReadDto>(result);
            }
            return null;
        }
    }
}