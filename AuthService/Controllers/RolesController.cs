using System.Collections.Generic;
using AuthService.Dtos;
using AuthService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public RolesController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IEnumerable<RoleReadDto>> GetRoles()
        {
            return Ok(_accountService.GetRoles());
        }
    }
}