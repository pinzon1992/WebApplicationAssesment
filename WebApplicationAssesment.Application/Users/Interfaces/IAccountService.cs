using WebApplicationAssesment.Application.Common.Interfaces;
using WebApplicationAssesment.Application.Users.Models;

namespace WebApplicationAssesment.Application.Users.Interfaces
{
    public interface IAccountService : IBaseServiceAsync<AccountDto, CreateAccountDto, long>
    {
        Task<AccountDto> Login(LoginDto login);
    }
}
