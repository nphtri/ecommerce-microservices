using AuthService.Dtos;
using AuthService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase 
    {
        private readonly IAuthenService _authService;

        public AuthController(IAuthenService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("access-token")]
        public ActionResult<AccessTokenResponseDto> GenerateAccessToken(AccessTokenGenerateDto dto)
        {
            var result = _authService.GenerateToken(dto.Username, dto.Password);
            if (result != null) {
                return result;
            }
            return Unauthorized();
        }
    }
}