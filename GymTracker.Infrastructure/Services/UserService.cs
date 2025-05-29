using AutoMapper;
using System.Security.Claims;
using GymTracker.Core.Entities;
using GymTracker.Application.Interfaces;
using GymTracker.Application.Interfaces.Repositories;
using GymTracker.Application.DTOs.User;

namespace GymTracker.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly IPasswordHasherService _passwordHasher;
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(ITokenService tokenService, IEmailService emailService, IPasswordHasherService passwordHasher, IUserRepository repository, IMapper mapper)
        {
            _tokenService = tokenService;
            _emailService = emailService;
            _passwordHasher = passwordHasher; 
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<string?> LoginAsync(UserLoginDto dto)
        {
            var user = await _repository.GetByEmailAsync(dto.Email);
            if (user == null) return null;

            if (!_passwordHasher.VerifyPassword(user.PasswordHash, dto.Password))
                return null;

            if (!user.IsActive)
                throw new InvalidOperationException("You must confirm your email before logging in.");

            var token = _tokenService.GenerateToken(user);
            return token;
        }

        public async Task RegisterAsync(UserRegisterDto dto)
        {
            if (await _repository.EmailExistsAsync(dto.Email))
                throw new InvalidOperationException("Email is already in use.");

            if (dto.Password.Length < 8 || !dto.Password.Any(char.IsUpper) || !dto.Password.Any(char.IsDigit))
                throw new InvalidOperationException("Password must be at least 8 characters, with one uppercase letter and one number.");

            var user = _mapper.Map<User>(dto);
            user.PasswordHash = _passwordHasher.HashPassword(dto.Password);
            user.IsActive = false;

            await _repository.AddAsync(user);
            await _repository.SaveChangesAsync();

            var token = _tokenService.GenerateEmailConfirmationToken(user.Email);
            await _emailService.SendConfirmationEmailAsync(user, token);
        }

        public async Task<bool> ConfirmEmailAsync(string token)
        {
            var email = _tokenService.ValidateEmailConfirmationToken(token);
            if (email == null)
                return false;

            var user = await _repository.GetByEmailAsync(email);
            if (user == null)
                return false;

            if (user.IsActive)
                return true;

            user.IsActive = true;
            await _repository.SaveChangesAsync();

            return true;
        }

        public async Task<UserUpdateDto?> GetCurrentUserAsync(ClaimsPrincipal userClaims)
        {
            var userIdClaim = userClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
                return null;

            var user = await _repository.GetByIdAsync(userId);
            return user == null ? null : _mapper.Map<UserUpdateDto>(user);
        }

        public async Task<bool> UpdateAsync(UserUpdateDto updateDto, ClaimsPrincipal userClaims)
        {
            var userIdClaim = userClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
                return false;

            var user = await _repository.GetByIdAsync(userId);
            if (user == null) return false;

            _mapper.Map(updateDto, user);

            await _repository.UpdateAsync(user);
            await _repository.SaveChangesAsync();

            return true;
        }

    }

}
