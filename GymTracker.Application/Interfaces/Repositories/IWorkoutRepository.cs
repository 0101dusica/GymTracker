using GymTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymTracker.Application.Interfaces.Repositories
{
    public interface IWorkoutRepository
    {
        Task AddAsync(WorkoutSession workout);
        Task<List<WorkoutSession>> GetByUserIdAndDateRangeAsync(Guid userId, DateTime start, DateTime end);
        Task SaveChangesAsync();
    }
}
