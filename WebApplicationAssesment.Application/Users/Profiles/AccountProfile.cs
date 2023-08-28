using AutoMapper;
using WebApplicationAssesment.Application.Users.Models;
using WebApplicationAssesment.Domain.Users;

namespace WebApplicationAssesment.Application.Users.Profiles
{
    public class AccountProfile : Profile
    {   
        public AccountProfile() 
        {
            CreateMap<Account, AccountDto>();
            CreateMap<AccountDto, Account>();
            CreateMap<CreateAccountDto, Account>();
        }
    }
}
