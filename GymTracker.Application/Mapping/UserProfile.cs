using GymTracker.Application.DTOs.User;
using GymTracker.Core.Entities;
using AutoMapper;

namespace GymTracker.Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponseDto>();
            CreateMap<UserRegisterDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())           
            .ForMember(dest => dest.IsActive, opt => opt.Ignore());
        }
    }
}