using GymTracker.Application.DTOs.Workout;
namespace GymTracker.Application.Interfaces
{
    public interface IWorkoutService
    {
        Task<WorkoutSessionDto> CreateAsync(Guid userId, WorkoutSessionDto dto);
        Task<List<WeeklyWorkoutSummaryDto>> GetMonthlySummaryAsync(Guid userId, DateTime referenceDate);
    }
}
