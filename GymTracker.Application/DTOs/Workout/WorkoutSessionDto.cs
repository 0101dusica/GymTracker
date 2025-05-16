using GymTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymTracker.Application.DTOs.Workout
{
    public class WorkoutSessionDto
    {
        public DateTime Timestamp { get; set; }
        public ExerciseType Type { get; set; }
        public int DurationMinutes { get; set; }
        public int CaloriesBurned { get; set; }
        public int Intensity { get; set; }
        public int FatigueLevel { get; set; }
        public string? Notes { get; set; }
    }

}
