using Microsoft.AspNetCore.Mvc;
using GymTracker.Application.Interfaces;
using GymTracker.Application.DTOs.User;

namespace GymTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            try
            {
                var token = await _userService.LoginAsync(dto);

                if (token == null)
                    return Unauthorized("Invalid credentials");

                return Ok(new { token });
            }
            catch (InvalidOperationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            try
            {
                await _userService.RegisterAsync(dto);
                return Ok(new { message = "Registration successful! Please check your email to confirm your account." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string token)
        {
            var result = await _userService.ConfirmEmailAsync(token);

            if (result)
            {
                return Redirect("http://localhost:4200/login");
            }

            return BadRequest(new { error = "Invalid or expired token." });
        }
    }
}