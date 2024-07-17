using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController:ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await _userService.RegisterAsync(registerDto);
            if (!result)
                return BadRequest("Email already exists.");

            return Ok("User registered successfully.");
     
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var token = await _userService.LoginAsync(loginDto);
            return Ok(new { Token = token });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(LogoutDto user)
        {
            var userId = user.UserId; 
            bool res = await _userService.LogoutAsync(userId);
            if (!res)
                return BadRequest("Logout unsuccessful");
            return Ok(new { Message = "Logout successful" });
        }
    }
}
