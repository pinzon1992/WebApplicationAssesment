using AutoMapper;
using WebApplicationAssesment.Application.Users.Models;
using WebApplicationAssesment.Domain.Users;

namespace WebApplicationAssesment.Application.Users.Profiles
{
    public class UserProfile : Profile
    {   
        public UserProfile() 
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<CreateUserDto, User>();
        }
    }
}
