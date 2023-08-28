using WebApplicationAssesment.Domain.Users;
using WebApplicationAssesment.Infraestructure.DBContext;
using WebApplicationAssesment.Infraestructure.Repositories.Users.Interfaces;
using WebApplicationAssesment.Infraestructure.Repositories.Users;


namespace WebApplicationAssesment.Tests.Infraestructure.Repositories
{
    public class RoleRepositoryTest
    {
        [Fact]
        public async Task RoleInsertTest_ShouldReturn_NewRoleWithId()
        {
            //Arrange
            TestSetup testSetup = new TestSetup();

            WebApplicationAssesmentContext context = new WebApplicationAssesmentContext(testSetup.Configuration);
            IRoleRepository roleRepository = new RoleRepository(context);
            Role role = new Role
            {
                Name = "Administrator",
            };
            //Act
            var result = await roleRepository.CreateAsync(role);

            //Assert
            Assert.IsType<Role>(result);
            Assert.True(result.Id > 0);
        }

        [Fact]
        public async Task RoleDeleteTest_ShouldReturn_DeletedRole()
        {
            //Arrange
            TestSetup testSetup = new TestSetup();

            WebApplicationAssesmentContext context = new WebApplicationAssesmentContext(testSetup.Configuration);
            IRoleRepository roleRepository = new RoleRepository(context);
            long id = 2;
            //Act
            var result = await roleRepository.DeleteAsync(id);

            //Assert
            Assert.IsType<Role>(result);
            Assert.True(result.IsDeleted);
        }

        [Fact]
        public async Task GetRoleByIdTest_ShouldReturn_NotDeletedRole()
        {
            //Arrange
            TestSetup testSetup = new TestSetup();

            WebApplicationAssesmentContext context = new WebApplicationAssesmentContext(testSetup.Configuration);
            IRoleRepository roleRepository = new RoleRepository(context);
            long id = 2;
            //Act
            var result = await roleRepository.GetByIdAsync(id);

            //Assert
            Assert.IsType<Role>(result);
            Assert.NotNull(result);
            Assert.True(!result.IsDeleted);
        }

        [Fact]
        public async Task GetAllRolesTest_ShouldReturn_RoleList()
        {
            //Arrange
            TestSetup testSetup = new TestSetup();

            WebApplicationAssesmentContext context = new WebApplicationAssesmentContext(testSetup.Configuration);
            IRoleRepository roleRepository = new RoleRepository(context);
            long id = 2;
            //Act
            var result = await roleRepository.GetAllAsync();

            //Assert
            Assert.IsType<List<Role>>(result);
            Assert.True(result.Count > 0);
        }

        [Fact]
        public async Task RoleUpdateTest_ShouldReturn_UpdatedRole()
        {
            //Arrange
            TestSetup testSetup = new TestSetup();

            WebApplicationAssesmentContext context = new WebApplicationAssesmentContext(testSetup.Configuration);
            IRoleRepository roleRepository = new RoleRepository(context);
            long id = 2;
            Role roleEntity = await roleRepository.GetByIdAsync(id);
            roleEntity.Name = "Administrator update";
            //Act

            Role updatedEntity = await roleRepository.UpdateAsync(roleEntity);

            //Assert
            Assert.IsType<Role>(updatedEntity);
            Assert.NotNull(updatedEntity);
            Assert.True(!updatedEntity.IsDeleted);
            Assert.Equal(roleEntity.Name, updatedEntity.Name);
        }
    }
}
