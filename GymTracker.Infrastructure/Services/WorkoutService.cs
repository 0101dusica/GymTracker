using AutoMapper;
using GymTracker.Application.DTOs.Workout;
using GymTracker.Application.Interfaces;
using GymTracker.Application.Interfaces.Repositories;
using GymTracker.Core.Entities;

namespace GymTracker.Infrastructure.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutRepository _repository;
        private readonly IMapper _mapper;

        public WorkoutService(IWorkoutRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<WorkoutSessionDto> CreateAsync(Guid userId, WorkoutSessionDto dto)
        {
            var workout = _mapper.Map<WorkoutSession>(dto);
            workout.Id = Guid.NewGuid();
            workout.UserId = userId;

            await _repository.AddAsync(workout);
            await _repository.SaveChangesAsync();

            return _mapper.Map<WorkoutSessionDto>(workout);
        }

        public async Task<List<WeeklyWorkoutSummaryDto>> GetMonthlySummaryAsync(Guid userId, DateTime referenceDate)
        {
            var monthStart = new DateTime(referenceDate.Year, referenceDate.Month, 1);
            var monthEnd = monthStart.AddMonths(1).AddDays(-1);

            var firstMonday = monthStart.AddDays(-(int)(monthStart.DayOfWeek == DayOfWeek.Sunday ? 6 : (monthStart.DayOfWeek - DayOfWeek.Monday)));

            int dayOfWeek = (int)monthEnd.DayOfWeek;
            if (dayOfWeek == 0)
                dayOfWeek = 7;

            var lastSunday = monthEnd.AddDays(7 - dayOfWeek);

            var workouts = await _repository.GetByUserIdAndDateRangeAsync(userId, firstMonday, lastSunday);

            var summary = new List<WeeklyWorkoutSummaryDto>();

            for (var weekStart = firstMonday; weekStart <= lastSunday; weekStart = weekStart.AddDays(7))
            {
                var weekEnd = weekStart.AddDays(6);

                var weeklyWorkouts = workouts.Where(w => w.Timestamp >= weekStart && w.Timestamp <= weekEnd).ToList();

                summary.Add(new WeeklyWorkoutSummaryDto
                {
                    Dates = $"{weekStart:dd.MM.} - {weekEnd:dd.MM.}",
                    TotalDuration = weeklyWorkouts.Sum(w => w.DurationMinutes),
                    WorkoutCount = weeklyWorkouts.Count,
                    AvgIntensity = weeklyWorkouts.Any() ? (int)weeklyWorkouts.Average(w => w.Intensity) : 0,
                    AvgFatigue = weeklyWorkouts.Any() ? (int)weeklyWorkouts.Average(w => w.FatigueLevel) : 0
                });
            }

            return summary;
        }
    }
}
