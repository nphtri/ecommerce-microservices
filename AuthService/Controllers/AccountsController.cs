using System;
using System.Collections.Generic;
using System.Security.Claims;
using AuthService.Dtos;
using AuthService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthenService _authService;
        private readonly IAccountService _accountService;

        public AccountsController(IAuthenService authService, IAccountService accountService)
        {
            _authService = authService;
            _accountService = accountService;
        }

        [Authorize(Roles="ADMIN")]
        [HttpGet]
        public ActionResult<IEnumerable<AccountReadDto>> GetAccounts()
        {
            return Ok(_accountService.GetAccounts());
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetAccountById")]
        public ActionResult<AccountReadDto> GetAccountById(int id)
        {
            if (!User.IsInRole("ADMIN")) {
                if (int.Parse(User.Identity.Name) != id) {
                    return Forbid();
                }
            }
            var result = _accountService.GetAccountById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [Authorize]
        [HttpGet("my-info")]
        public ActionResult<AccountReadDto> GetAccountByToken()
        {
            var result = _accountService.GetAccountById(int.Parse(User.Identity.Name));
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult<AccountReadDto> Register(RegisterDto dto)
        {
            var exist = _accountService.GetAccountByUserName(dto.Username);
            if (exist != null)
            {
                return Conflict(new { Message = "Username is existed" });
            }
            if (!string.IsNullOrEmpty(dto.Email))
            {
                exist = _accountService.GetAccountByEmail(dto.Email);
                if (exist != null)
                {
                    return Conflict(new { Message = "Email is existed" });
                }
            }
            if (!string.IsNullOrEmpty(dto.Phone))
            {
                exist = _accountService.GetAccountByPhone(dto.Phone);
                if (exist != null)
                {
                    return Conflict(new { Message = "Phone is existed" });
                }
            }

            var result = _authService.Register(dto);
            if (result != null)
            {
                return CreatedAtRoute(nameof(GetAccountById), new { id = result.Id }, result);
            }
            return BadRequest();
        }
    }
}