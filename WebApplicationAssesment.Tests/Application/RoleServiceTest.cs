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
    public class RoleServiceTest
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public RoleServiceTest()
        {
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

        }

        [Fact]
        public async Task CreateRoleAsync ()
        {
            //arrange
            IRoleService roleService = new RoleService(_roleRepository, _mapper);
            CreateRoleDto role = new CreateRoleDto
            {
                Name = "Visitor"
            };
            //act
            var result = await roleService.CreateAsync(role);

            //assert
            Assert.IsType<RoleDto>(result);
        }

        [Fact]
        public async Task DeleteRoleAsync()
        {
            //arrange
            IRoleService roleService = new RoleService(_roleRepository, _mapper);
            long id = 5;
            //act
            var result = await roleService.DeleteByIdAsync(id);

            //assert
            Assert.IsType<RoleDto>(result);
            Assert.True(result.IsDeleted);
        }
    }
}
