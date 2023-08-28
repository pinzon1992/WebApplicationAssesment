using Microsoft.Extensions.Configuration;
using System.Data;


namespace WebApplicationAssesment.Infraestructure.DBContext
{
    public class WebApplicationAssesmentContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public WebApplicationAssesmentContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public IDbConnection CreateConnection()
            => new Npgsql.NpgsqlConnection(_connectionString);
    }
}
