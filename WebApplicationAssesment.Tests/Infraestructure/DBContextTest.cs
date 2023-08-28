using Npgsql;
using WebApplicationAssesment.Infraestructure.DBContext;


namespace WebApplicationAssesment.Tests.Infraestructure
{
    public class DBContextTest
    {

        [Fact]
        public void GetConnectionTest()
        {
            //Arrange
            TestSetup testSetup = new TestSetup();

            WebApplicationAssesmentContext context = new WebApplicationAssesmentContext(testSetup.Configuration);
            //Act
            var result = context.CreateConnection();

            //Assert
            Assert.IsType<NpgsqlConnection>(result);
        }

        
    }
}