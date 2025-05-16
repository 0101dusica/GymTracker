using System;
using Microsoft.EntityFrameworkCore;
using GymTracker.Core.Entities;
using System.Reflection.Emit;

namespace GymTracker.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<WorkoutSession> WorkoutSessions => Set<WorkoutSession>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.WorkoutSessions)
                .WithOne(w => w.User)
                .HasForeignKey(w => w.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
