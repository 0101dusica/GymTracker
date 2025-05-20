using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymTracker.Application.DTOs.Workout
{
    public class WeeklyWorkoutSummaryDto
    {
        public string Dates { get; set; }
        public int TotalDuration { get; set; }
        public int WorkoutCount { get; set; }
        public int AvgIntensity { get; set; }
        public int AvgFatigue { get; set; }
    }

}
