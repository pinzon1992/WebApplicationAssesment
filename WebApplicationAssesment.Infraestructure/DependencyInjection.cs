using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplicationAssesment.Infraestructure.DBContext;
using WebApplicationAssesment.Infraestructure.Repositories.Accounts;
using WebApplicationAssesment.Infraestructure.Repositories.Users;
using WebApplicationAssesment.Infraestructure.Repositories.Users.Interfaces;

namespace WebApplicationAssesment.Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<WebApplicationAssesmentContext>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            return services;
        }
    }
}
