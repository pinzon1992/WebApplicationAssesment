using AutoMapper;
using WebApplicationAssesment.Application.Users;
using WebApplicationAssesment.Application.Users.Interfaces;
using WebApplicationAssesment.Application.Users.Models;
using WebApplicationAssesment.Domain.Users;
using WebApplicationAssesment.Infraestructure.DBContext;
using WebApplicationAssesment.Infraestructure.Repositories.Accounts;
using WebApplicationAssesment.Infraestructure.Repositories.Users;
using WebApplicationAssesment.Infraestructure.Repositories.Users.Interfaces;

namespace WebApplicationAssesment.Tests.Application
{
    public class AccountServiceTest
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public AccountServiceTest()
        {
            TestSetup testSetup = new TestSetup();

            WebApplicationAssesmentContext context = new WebApplicationAssesmentContext(testSetup.Configuration);
            _accountRepository = new AccountRepository(context);
            _userRepository = new UserRepository(context);

            var config = new MapperConfiguration(cfg => 
            { 
                cfg.CreateMap<Account, AccountDto>();
                cfg.CreateMap<AccountDto, Account>();
                cfg.CreateMap<CreateAccountDto, Account>();
            });
            _mapper = config.CreateMapper();

        }

        [Fact]
        public async Task CreateAccountAsync ()
        {
            //arrange
            IAccountService accountService = new AccountService(_accountRepository, _userRepository, _mapper);
            CreateAccountDto account = new CreateAccountDto
            {
                Username = "jp_1992@yopmail.com",
                Password = "jp_1992",
            };
            //act
            var result = await accountService.CreateAsync(account);

            //assert
            Assert.IsType<AccountDto>(result);
        }

        [Fact]
        public async Task DeleteAccountAsync()
        {
            //arrange
            IAccountService accountService = new AccountService(_accountRepository,_userRepository, _mapper);
            long id = 5;
            //act
            var result = await accountService.DeleteByIdAsync(id);

            //assert
            Assert.IsType<AccountDto>(result);
            Assert.True(result.IsDeleted);
        }
    }
}
