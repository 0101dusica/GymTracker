using GymTracker.Application.DTOs.User;
using GymTracker.Core.Entities;
using AutoMapper;

namespace GymTracker.Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserUpdateDto>();

            CreateMap<UserUpdateDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Email, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) 
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.WorkoutSessions, opt => opt.Ignore()); 

            CreateMap<UserRegisterDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())         
            .ForMember(dest => dest.IsActive, opt => opt.Ignore());
        }
    }
}