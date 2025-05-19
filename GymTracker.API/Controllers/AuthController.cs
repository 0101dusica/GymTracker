using GymTracker.Application.DTOs.User;
using GymTracker.Application.Interfaces;
using GymTracker.Application.Services;
using GymTracker.Core.Entities;
using GymTracker.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
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
        private readonly IEmailService _emailService;

        public AuthController(AppDbContext db, ITokenService tokenService, IEmailService emailService)
        {
            _db = db;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null) return Unauthorized("Invalid credentials");

            // TODO: Compare with hashed password not just plain text
            if (user.PasswordHash != dto.Password) return Unauthorized("Invalid credentials");

            if (!user.IsActive)
                return Unauthorized("You must confirm your email before logging in.");

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

            var token = _tokenService.GenerateEmailConfirmationToken(dto.Email);
            var confirmationLink = $"http://localhost:5000/api/auth/confirm-email?token={token}";

            await _emailService.SendEmailAsync(dto.Email, "Confirm your email", $@"
                <html>
                  <body style='font-family: Arial, sans-serif; background-color: #f4f4f4; color: #333; padding: 20px;'>
                    <div style='max-width: 600px; margin: 0 auto; background-color: #ffffff; padding: 30px; border-radius: 10px; box-shadow: 0 4px 8px rgba(0,0,0,0.1); text-align: center;'>
                      <h2 style='color: #6199bc;'>Welcome to GymTracker!</h2>
                      <p>Hi {dto.FirstName},</p>
                      <p>Thank you for registering with <strong>GymTracker</strong>, your companion for tracking workouts and monitoring progress.</p>
                      <p>To complete your registration, please confirm your email by clicking the link below:</p>
                      <div style='margin: 30px 0;'>
                        <a href='{confirmationLink}' style='background-color: #6199bc; color: white; padding: 12px 24px; border-radius: 5px; text-decoration: none; font-weight: bold;'>Confirm Email</a>
                      </div>
                      <p>If you didn't create an account with GymTracker, please ignore this email.</p>
                      <p style='margin-top: 30px;'>Stay consistent and strong,<br/>The GymTracker Team 💪</p>
                    </div>
                  </body>
                </html>");

            return Ok(new { message = "Registration successful! Please check your email to confirm your account." });
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string token)
        {
            var email = _tokenService.ValidateEmailConfirmationToken(token);

            if (email == null)
                return BadRequest("Invalid or expired token.");

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return NotFound("User not found.");

            if (user.IsActive)
                return Ok("Email already confirmed.");

            user.IsActive = true;
            await _db.SaveChangesAsync();

            return Ok(new { message = "Email successfully confirmed!" });
            //return Redirect("http://localhost:4200/login"); 
        }


    }

}
