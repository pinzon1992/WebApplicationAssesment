using Microsoft.Extensions.Configuration;

namespace WebApplicationAssesment.Tests
{
    public class TestSetup
    {
        public IConfigurationRoot Configuration { get; set; }
        public TestSetup() 
        {
            Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        }
    }
}
