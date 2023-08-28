using WebApplicationAssesment.Domain.Users;
using WebApplicationAssesment.Infraestructure.DBContext;
using WebApplicationAssesment.Infraestructure.Repositories.Users.Interfaces;
using WebApplicationAssesment.Infraestructure.Repositories.Users;


namespace WebApplicationAssesment.Tests.Infraestructure.Repositories
{
    public class UserRepositoryTest
    {
        [Fact]
        public async Task UserInsertTest_ShouldReturn_UserWithId()
        {
            //Arrange
            TestSetup testSetup = new TestSetup();

            WebApplicationAssesmentContext context = new WebApplicationAssesmentContext(testSetup.Configuration);
            IUserRepository userRepository = new UserRepository(context);
            User user = new User
            {
                Firstname = "Juan",
                Lastname = "Pinzon",
                AccountId = 3
            };
            //Act
            var result = await userRepository.CreateAsync(user);

            //Assert
            Assert.IsType<User>(result);
            Assert.True(result.Id > 0);
        }

        [Fact]
        public async Task UserDeleteTest_ShouldReturn_UserDeleted()
        {
            //Arrange
            TestSetup testSetup = new TestSetup();

            WebApplicationAssesmentContext context = new WebApplicationAssesmentContext(testSetup.Configuration);
            IUserRepository userRepository = new UserRepository(context);
            long id = 1;
            //Act
            var result = await userRepository.DeleteAsync(id);

            //Assert
            Assert.IsType<User>(result);
            Assert.True(result.IsDeleted);
        }

        [Fact]
        public async Task GetUserByIdTest_ShouldReturn_NotDeletedUser()
        {
            //Arrange
            TestSetup testSetup = new TestSetup();

            WebApplicationAssesmentContext context = new WebApplicationAssesmentContext(testSetup.Configuration);
            IUserRepository userRepository = new UserRepository(context);
            long id = 1;
            //Act
            var result = await userRepository.GetByIdAsync(id);

            //Assert
            Assert.IsType<User>(result);
            Assert.NotNull(result);
            Assert.True(!result.IsDeleted);
        }

        [Fact]
        public async Task GetAllUsersTest_ShouldReturn_UserList()
        {
            //Arrange
            TestSetup testSetup = new TestSetup();

            WebApplicationAssesmentContext context = new WebApplicationAssesmentContext(testSetup.Configuration);
            IUserRepository userRepository = new UserRepository(context);
            long id = 1;
            //Act
            var result = await userRepository.GetAllAsync();

            //Assert
            Assert.IsType<List<User>>(result);
            Assert.True(result.Count > 0);
        }

        [Fact]
        public async Task UserUpdateTest_ShouldReturn_UpdatedUser()
        {
            //Arrange
            TestSetup testSetup = new TestSetup();

            WebApplicationAssesmentContext context = new WebApplicationAssesmentContext(testSetup.Configuration);
            IUserRepository userRepository = new UserRepository(context);
            long id = 1;
            User userEntity = await userRepository.GetByIdAsync(id);
            userEntity.Firstname = "Juan 1";
            userEntity.Lastname = "Pinzon 1";
            //Act

            User updatedEntity = await userRepository.UpdateAsync(userEntity);

            //Assert
            Assert.IsType<User>(updatedEntity);
            Assert.NotNull(updatedEntity);
            Assert.True(!updatedEntity.IsDeleted);
            Assert.Equal(userEntity.Firstname, updatedEntity.Firstname);
        }
    }
}
