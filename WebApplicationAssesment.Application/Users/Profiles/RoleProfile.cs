using AutoMapper;
using WebApplicationAssesment.Application.Users.Models;
using WebApplicationAssesment.Domain.Users;

namespace WebApplicationAssesment.Application.Users.Profiles
{
    public class RoleProfile : Profile
    {   
        public RoleProfile() 
        {
            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();
            CreateMap<CreateRoleDto, Role>();
        }
    }
}
