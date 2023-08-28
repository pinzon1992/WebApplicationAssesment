using WebApplicationAssesment.Domain.Common.Helpers;

namespace WebApplicationAssesment.Tests.Domain.Helpers
{
    public class PasswordHasherTest
    {
        [Fact]
        public void HashPassword_ShouldReturn_True_IfPasswordIsHashed()
        {
            //Arrange
            string password = "test123";
            
            //Act
            string hash = PasswordHasher.HashPasword(password, null, out var salt);

            //Assert
            Assert.True(!hash.Equals(password));
            Assert.True(PasswordHasher.VerifyPassword(password, hash, salt));

        }

        [Fact]
        public void ValidatePassword_ShouldReturn_IfPasswordIsValid()
        {
            //Arrange
            string password = "jp_1992";
            string storedSalt = "D12B99B0BA7DBB12E2E4AE23A31998F9BBBCD0DF92F3DA5D07419FADEB30DE0713675F7F28C9424C70189A73DACC15D4E7AC683BFA9FE023C4BD42EF432463E5";
            byte[] byteSalt = Convert.FromHexString(storedSalt);
            //Act
            string hash = PasswordHasher.HashPasword(password, byteSalt, out var salt);
            //Assert
            Assert.True(PasswordHasher.VerifyPassword(password, hash, byteSalt));

        }
    }
}
