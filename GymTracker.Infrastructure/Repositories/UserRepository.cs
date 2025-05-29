using Microsoft.EntityFrameworkCore;
using GymTracker.Core.Entities;
using GymTracker.Infrastructure.Persistence;
using GymTracker.Application.Interfaces.Repositories;

namespace GymTracker.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _db.Users.AnyAsync(u => u.Email == email);
        }

        public UserRepository(AppDbContext context)
        {
            _db = context;
        }

        public async Task<User?> GetByIdAsync(Guid id)
            => await _db.Users.FindAsync(id);

        public async Task<User?> GetByEmailAsync(string email)
            => await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<IEnumerable<User>> GetAllAsync()
            => await _db.Users.ToListAsync();

        public async Task AddAsync(User user)
            => await _db.Users.AddAsync(user);

        public async Task UpdateAsync(User user)
            => _db.Users.Update(user);

        public async Task DeleteAsync(User user)
            => _db.Users.Remove(user);

        public async Task SaveChangesAsync()
            => await _db.SaveChangesAsync();
    }

}
