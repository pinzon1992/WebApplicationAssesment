using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApplicationAssesment.Application.Users;
using WebApplicationAssesment.Application.Users.Interfaces;
using WebApplicationAssesment.Application.Users.Models;
using WebApplicationAssesment.Controllers.Users;
using WebApplicationAssesment.Domain.Users;
using WebApplicationAssesment.Infraestructure.DBContext;
using WebApplicationAssesment.Infraestructure.Repositories.Users;
using WebApplicationAssesment.Infraestructure.Repositories.Users.Interfaces;

namespace WebApplicationAssesment.Tests.Presentation.Controllers
{
    public class RoleControllerTest
    {
        private readonly IRoleService _roleService;
        private readonly IRoleRepository _roleRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public RoleControllerTest()
        {
            var serviceProvider = new ServiceCollection()
            .AddLogging()
            .BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            _logger = factory.CreateLogger<RoleController>();

            TestSetup testSetup = new TestSetup();

            WebApplicationAssesmentContext context = new WebApplicationAssesmentContext(testSetup.Configuration);
            _roleRepository = new RoleRepository(context);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Role, RoleDto>();
                cfg.CreateMap<RoleDto, Role>();
                cfg.CreateMap<CreateRoleDto, Role>();
            });
            _mapper = config.CreateMapper();
            _configuration = testSetup.Configuration;
            _roleService = new RoleService(_roleRepository, _mapper);

        }

        [Fact]
        public async Task CreateRoleAsyncTest_ShouldReturn_OkResponse()
        {
            //Arrange
            RoleController roleController = new RoleController(_roleService, _logger, _configuration);
            //Act 
            CreateRoleDto createRole = new CreateRoleDto
            {
                Name = "Viewer"
            };
            var result = await roleController.Create(createRole);
            var okResult = result as OkObjectResult;
            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
        [Fact]
        public async Task DeleteRoleAsyncTest_ShouldReturn_OkResponse()
        {
            //Arrange
            RoleController roleController = new RoleController(_roleService, _logger, _configuration);
            //Act 
            long id = 2;
            var result = await roleController.Delete(id);
            var okResult = result as OkObjectResult;
            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
        [Fact]
        public async Task GetRoleBydIdAsyncTest_ShouldReturn_OkResponse()
        {
            //Arrange
            RoleController roleController = new RoleController(_roleService, _logger, _configuration);
            //Act 
            long id = 2;
            var result = await roleController.Get(id);
            var okResult = result as OkObjectResult;
            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetAllRolesAsyncTest_ShouldReturn_OkResponse()
        {
            //Arrange
            RoleController roleController = new RoleController(_roleService, _logger, _configuration);
            //Act 
            var result = await roleController.GetAll();
            var okResult = result as OkObjectResult;
            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task UpdateRoleAsyncTest_ShouldReturn_OkResponse()
        {
            //Arrange
            RoleController roleController = new RoleController(_roleService, _logger, _configuration);
            //Act 
            RoleDto role = new RoleDto
            {
                Id = 2,
                Name = "Viewer"
            };
            var result = await roleController.Update(role);
            var okResult = result as OkObjectResult;
            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
