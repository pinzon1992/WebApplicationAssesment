using WebApplicationAssesment.Application.Common.Interfaces;
using WebApplicationAssesment.Application.Users.Models;

namespace WebApplicationAssesment.Application.Users.Interfaces
{
    public interface IRoleService : IBaseServiceAsync<RoleDto, CreateRoleDto, long>
    {
    }
}
