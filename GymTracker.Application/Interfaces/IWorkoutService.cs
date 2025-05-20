using GymTracker.Application.DTOs.Workout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymTracker.Application.Interfaces
{
    public interface IWorkoutService
    {
        Task<List<WeeklyWorkoutSummaryDto>> GetMonthlySummaryAsync(Guid userId, DateTime referenceDate);
    }
}
