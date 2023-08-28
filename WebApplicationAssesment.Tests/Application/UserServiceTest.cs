using AutoMapper;
using WebApplicationAssesment.Application.Users;
using WebApplicationAssesment.Application.Users.Interfaces;
using WebApplicationAssesment.Application.Users.Models;
using WebApplicationAssesment.Domain.Users;
using WebApplicationAssesment.Infraestructure.DBContext;
using WebApplicationAssesment.Infraestructure.Repositories.Users;
using WebApplicationAssesment.Infraestructure.Repositories.Users.Interfaces;

namespace WebApplicationAssesment.Tests.Application
{
    public class UserServiceTest
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserServiceTest()
        {
            TestSetup testSetup = new TestSetup();

            WebApplicationAssesmentContext context = new WebApplicationAssesmentContext(testSetup.Configuration);
            _userRepository = new UserRepository(context);

            var config = new MapperConfiguration(cfg => 
            { 
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserDto, User>();
                cfg.CreateMap<CreateUserDto, User>();
            });
            _mapper = config.CreateMapper();

        }

        [Fact]
        public async Task CreateUserAsync ()
        {
            //arrange
            IUserService userService = new UserService(_userRepository, _mapper);
            CreateUserDto user = new CreateUserDto
            {
                Firstname = "Juan",
                Lastname = "Pinzon",
                AccountId = 7
            };
            //act
            var result = await userService.CreateAsync(user);

            //assert
            Assert.IsType<UserDto>(result);
        }

        [Fact]
        public async Task DeleteUserAsync()
        {
            //arrange
            IUserService userService = new UserService(_userRepository, _mapper);
            long id = 5;
            //act
            var result = await userService.DeleteByIdAsync(id);

            //assert
            Assert.IsType<UserDto>(result);
            Assert.True(result.IsDeleted);
        }
    }
}
