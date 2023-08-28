using WebApplicationAssesment.Application.Common.Interfaces;
using WebApplicationAssesment.Application.Users.Models;

namespace WebApplicationAssesment.Application.Users.Interfaces
{
    public interface IUserService : IBaseServiceAsync<UserDto, CreateUserDto, long>
    {
        Task<UserDto> GetByAccountIdAsync(long accountId);
    }
}
