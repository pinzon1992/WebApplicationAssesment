using WebApplicationAssesment.Domain.Users;

namespace WebApplicationAssesment.Tests.Domain.Accounts
{
    public class AccountTest
    {
        [Fact]
        public void InvalidUsernameTest_ShouldReturn_IsInvalidUsername()
        {
            //Arrange
            Account account = new Account
            {
                Username = "Test",
            };

            //Act
            bool isValid = account.IsValidUsername();

            //Assert
            Assert.True(!isValid);
        }

        [Fact]
        public void ValidUsernameTest_ShouldReturn_IsValidUsername()
        {
            //Arrange
            Account account = new Account
            {
                Username = "jp_1992@yopmail.com",
            };

            //Act
            bool isValid = account.IsValidUsername();

            //Assert
            Assert.True(isValid);
        }
    }
}
