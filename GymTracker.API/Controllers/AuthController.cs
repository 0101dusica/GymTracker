using GymTracker.Application.DTOs.User;
using GymTracker.Application.Interfaces;
using GymTracker.Core.Entities;
using GymTracker.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymTracker.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ITokenService _tokenService;

        public AuthController(AppDbContext db, ITokenService tokenService)
        {
            _db = db;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null) return Unauthorized("Invalid credentials");

            // Uporedi password (ovde je plaintext, u realnosti koristi hash proveru)
            if (user.PasswordHash != dto.Password) return Unauthorized("Invalid credentials");

            var token = _tokenService.GenerateToken(user);
            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            if (await _db.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("Email is already in use.");

            if (dto.Password.Length < 8 || !dto.Password.Any(char.IsUpper) || !dto.Password.Any(char.IsDigit))
                return BadRequest("Password must be at least 8 characters, with one uppercase letter and one number.");

            var user = new User
            {
                Email = dto.Email,
                PasswordHash = dto.Password, // TODO: Hash the password before saving
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                IsActive = false
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            // TODO: Send confirmation email with unique link/token
            return Ok(new { message = "Registration successful! Please check your email to confirm your account." });
        }
    }

}
