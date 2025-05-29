using GymTracker.Application.DTOs.User;
using System.Security.Claims;

namespace GymTracker.Application.Interfaces
{
    public interface IUserService
    {
        Task<string?> LoginAsync(UserLoginDto dto);
        Task RegisterAsync(UserRegisterDto dto);
        public Task<bool> ConfirmEmailAsync(string token);

        Task<UserUpdateDto?> GetCurrentUserAsync(ClaimsPrincipal userClaims);
        Task<bool> UpdateAsync(UserUpdateDto updateDto, ClaimsPrincipal userClaims);
    }
}

