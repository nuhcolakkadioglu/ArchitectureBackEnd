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
        public IActionResult Register([FromBody] RegisterAuthDto user)
        {
            _authService.Register(user);
            return Ok(new { message = "User added" });
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginAuthDto user)
        {
           var result = _authService.Login(user);
            return Ok(new { message = result });
        }
    }
}
