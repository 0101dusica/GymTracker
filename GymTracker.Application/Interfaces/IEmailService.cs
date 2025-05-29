using GymTracker.Core.Entities;

namespace GymTracker.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendConfirmationEmailAsync(User user, string token);
    }
}
