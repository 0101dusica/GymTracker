using Microsoft.EntityFrameworkCore;
using GymTracker.Core.Entities;
using GymTracker.Infrastructure.Persistence;
using GymTracker.Application.Interfaces.Repositories;

namespace GymTracker.Infrastructure.Repositories
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly AppDbContext _db;

        public WorkoutRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(WorkoutSession workout)
        {
            await _db.WorkoutSessions.AddAsync(workout);
        }

        public async Task<List<WorkoutSession>> GetByUserIdAndDateRangeAsync(Guid userId, DateTime start, DateTime end)
        {
            return await _db.WorkoutSessions
                .Where(w => w.UserId == userId && w.Timestamp >= start && w.Timestamp <= end).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
