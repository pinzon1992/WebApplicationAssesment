using WebApplicationAssesment.Infraestructure.DBContext;
using WebApplicationAssesment.Infraestructure.Repositories.Users.Interfaces;
using WebApplicationAssesment.Domain.Users;
using WebApplicationAssesment.Infraestructure.Repositories.Accounts;

namespace WebApplicationAssesment.Tests.Infraestructure.Repositories
{
    public class AccountRepositoryTest
    {
        [Fact]
        public async Task AccountInsertTest_ShouldReturn_NewUserWithId()
        {
            //Arrange
            TestSetup testSetup = new TestSetup();

            WebApplicationAssesmentContext context = new WebApplicationAssesmentContext(testSetup.Configuration);
            IAccountRepository accountRepository = new AccountRepository(context);
            Account account = new Account
            {
                Username = "jp_1992@yopmail.com",
                Password = "test",
                RoleId = 2
            };
            //Act
            var result = await accountRepository.CreateAsync(account);

            //Assert
            Assert.IsType<Account>(result);
            Assert.True(result.Id > 0);
        }

        [Fact]
        public async Task AccountDeleteTest()
        {
            //Arrange
            TestSetup testSetup = new TestSetup();

            WebApplicationAssesmentContext context = new WebApplicationAssesmentContext(testSetup.Configuration);
            IAccountRepository accountRepository = new AccountRepository(context);
            long id = 3;
            //Act
            var result = await accountRepository.DeleteAsync(id);

            //Assert
            Assert.IsType<Account>(result);
            Assert.True(result.IsDeleted);
        }

        [Fact]
        public async Task GetAccountByIdTest_ShouldReturn_Account_NotDeleted()
        {
            //Arrange
            TestSetup testSetup = new TestSetup();

            WebApplicationAssesmentContext context = new WebApplicationAssesmentContext(testSetup.Configuration);
            IAccountRepository accountRepository = new AccountRepository(context);
            long id = 4;
            //Act
            var result = await accountRepository.GetByIdAsync(id);

            //Assert
            Assert.IsType<Account>(result);
            Assert.NotNull(result);
            Assert.True(!result.IsDeleted);
        }

        [Fact]
        public async Task GetAllAccountsTest_ShouldReturn_AccountList()
        {
            //Arrange
            TestSetup testSetup = new TestSetup();

            WebApplicationAssesmentContext context = new WebApplicationAssesmentContext(testSetup.Configuration);
            IAccountRepository accountRepository = new AccountRepository(context);
            long id = 1;
            //Act
            var result = await accountRepository.GetAllAsync();

            //Assert
            Assert.IsType<List<Account>>(result);
            Assert.True(result.Count > 0);
        }

        [Fact]
        public async Task AccountUpdateTest_ShouldReturn_UpdatedAccount()
        {
            //Arrange
            TestSetup testSetup = new TestSetup();

            WebApplicationAssesmentContext context = new WebApplicationAssesmentContext(testSetup.Configuration);
            IAccountRepository accountRepository = new AccountRepository(context);
            long id = 4;
            Account accountEntity = await accountRepository.GetByIdAsync(id);
            accountEntity.Username = "jp_1992@yopmail.com 1";
            accountEntity.Password = "teest12";
            accountEntity.RoleId = 2;
            //Act

            Account updatedEntity = await accountRepository.UpdateAsync(accountEntity);

            //Assert
            Assert.IsType<Account>(updatedEntity);
            Assert.NotNull(updatedEntity);
            Assert.True(!updatedEntity.IsDeleted);
            Assert.Equal(accountEntity.Username, updatedEntity.Username);
        }
    }
}
