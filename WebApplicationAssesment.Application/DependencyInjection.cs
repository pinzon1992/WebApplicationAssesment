
using Microsoft.Extensions.DependencyInjection;
using WebApplicationAssesment.Application.Users;
using WebApplicationAssesment.Application.Users.Interfaces;
using WebApplicationAssesment.Application.Users.Profiles;

namespace WebApplicationAssesment.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IUserService, UserService>();

            services.AddAutoMapper(
                typeof(RoleProfile),
                typeof(UserProfile),
                typeof(AccountProfile)
            );
            return services;
        }
    }
}
