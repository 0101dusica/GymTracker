using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymTracker.Core
{
    public enum ExerciseType
    {
        Cardio,
        Strength,
        Flexibility,
        Other
    }

    public class WorkoutSession
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public ExerciseType Type { get; set; }  // Enum
        public int DurationMinutes { get; set; }
        public int CaloriesBurned { get; set; }

        public int Intensity { get; set; } // 1–10
        public int FatigueLevel { get; set; } // 1–10
        public string? Notes { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }

}
