using GymTracker.Core.Entities;

namespace GymTracker.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
