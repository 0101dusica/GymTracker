using GymTracker.Application.DTOs.Workout;
using GymTracker.Core.Entities;
using AutoMapper;

namespace GymTracker.Application.Mapping
{
    public class WorkoutProfile : Profile
    {
        public WorkoutProfile()
        {
            CreateMap<WorkoutSession, WorkoutSessionDto>().ReverseMap();
        }
    }
}