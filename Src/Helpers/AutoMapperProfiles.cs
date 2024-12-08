using AutoMapper;
using dotnet_exam2.Src.DTOs;
using dotnet_exam2.Src.Entities;

namespace dotnet_exam2.Src.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserCreateDto, User>();

            CreateMap<User, UserResponseDto>();

            CreateMap<Gender, GenderDto>();
        }
    }
}
