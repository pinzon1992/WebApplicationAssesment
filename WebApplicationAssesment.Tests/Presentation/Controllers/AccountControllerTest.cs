using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApplicationAssesment.Application.Users.Interfaces;
using WebApplicationAssesment.Application.Users;
using WebApplicationAssesment.Application.Users.Models;
using WebApplicationAssesment.Infraestructure.DBContext;
using WebApplicationAssesment.Infraestructure.Repositories.Users.Interfaces;
using WebApplicationAssesment.Controllers.Accounts;
using WebApplicationAssesment.Infraestructure.Repositories.Accounts;
using WebApplicationAssesment.Domain.Users;
using WebApplicationAssesment.Infraestructure.Repositories.Users;

namespace WebApplicationAssesment.Tests.Presentation.Controllers
{
    public class AccountControllerTest
    {
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AccountControllerTest()
        {
            var serviceProvider = new ServiceCollection()
            .AddLogging()
            .BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            _logger = factory.CreateLogger<AccountController>();

            TestSetup testSetup = new TestSetup();

            WebApplicationAssesmentContext context = new WebApplicationAssesmentContext(testSetup.Configuration);
            _accountRepository = new AccountRepository(context);
            _userRepository = new UserRepository(context);
            _roleRepository = new RoleRepository(context);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Account, AccountDto>();
                cfg.CreateMap<AccountDto, Account>();
                cfg.CreateMap<CreateAccountDto, Account>();

                cfg.CreateMap<Role, RoleDto>();
                cfg.CreateMap<RoleDto, Role>();
                cfg.CreateMap<CreateRoleDto, Role>();

                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserDto, User>();
                cfg.CreateMap<CreateUserDto, User>();
            });
            _mapper = config.CreateMapper();
            _configuration = testSetup.Configuration;
            _accountService = new AccountService(_accountRepository, _userRepository, _mapper);
            _roleService = new RoleService(_roleRepository, _mapper);
            _userService = new UserService(_userRepository, _mapper);

        }
        [Fact]
        public async Task CreateAccountAsyncTest_ShouldReturn_OkResponse()
        {
            //Arrange
            AccountController accountController = new AccountController(_accountService, _roleService, _userService, _logger, _configuration);
            CreateAccountDto createAccount = new CreateAccountDto
            {
                Username = "jp_19922@yopmail.com",
                Password = "jp_19922",
                FirstName = "Juan 2",
                LastName = "Pinzon 2",
                RoleId = 2
            };

            //Act 
            var result = await accountController.Create(createAccount);
            var okResult = result as OkObjectResult;
            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
        [Fact]
        public async Task DeleteAccountAsyncTest_ShouldReturn_OkResponse()
        {
            //Arrange
            AccountController accountController = new AccountController(_accountService, _roleService, _userService, _logger, _configuration);
            //Act 
            long id = 2;
            var result = await accountController.Delete(id);
            var okResult = result as OkObjectResult;
            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
        [Fact]
        public async Task GetAccountBydIdAsyncTest_ShouldReturn_OkResponse()
        {
            //Arrange
            AccountController accountController = new AccountController(_accountService, _roleService, _userService, _logger, _configuration);
            //Act 
            long id = 2;
            var result = await accountController.Get(id);
            var okResult = result as OkObjectResult;
            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetAllAccountsAsyncTest_ShouldReturn_OkResponse()
        {
            //Arrange
            AccountController accountController = new AccountController(_accountService, _roleService, _userService, _logger, _configuration);
            //Act 
            var result = await accountController.GetAll();
            var okResult = result as OkObjectResult;
            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task UpdateAccountAsyncTest_ShouldReturn_OkResponse()
        {
            //Arrange
            AccountController accountController = new AccountController(_accountService, _roleService, _userService, _logger, _configuration);
            //Act 
            AccountDto account = new AccountDto
            {
                Id = 2,
                Username = "jp_19921@yopmail.com",
                Password = "password"
            };
            var result = await accountController.Update(account);
            var okResult = result as OkObjectResult;
            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
