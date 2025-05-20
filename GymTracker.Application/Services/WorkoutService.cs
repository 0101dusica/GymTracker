using GymTracker.Application.DTOs.Workout;
using GymTracker.Application.Interfaces;
using GymTracker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymTracker.Application.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly AppDbContext _context;

        public WorkoutService(AppDbContext context)
        {
            _context = context;
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

            var summary = new List<WeeklyWorkoutSummaryDto>();
            var workouts = await _context.WorkoutSessions
                .Where(w => w.UserId == userId && w.Timestamp >= monthStart && w.Timestamp <= monthEnd)
                .ToListAsync();

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
