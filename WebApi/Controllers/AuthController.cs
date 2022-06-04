using Business.Abstract;
using Entities.Dtos.AuthDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("Register")]
        public IActionResult Register([FromForm] RegisterAuthDto user)
        {
            var result = _authService.Register(user);
            if (result.Success)
                return Ok(new { message = result });

            return BadRequest(result);
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginAuthDto user)
        {
            var result = _authService.Login(user);
            return Ok(new { message = result });
        }
    }
}
