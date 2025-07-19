using Auth.Applicatoin.BusinessInterfaces;
using Auth.Applicatoin.BusinessLogic;
using Auth.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Applicatoin
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthProcessor, AuthProcessor>();
            services.AddScoped<IUserManagerProcessor, UserManagerProcessor>();

            //services.AddIdentity<ApplicationUser, IdentityRole>();
            return services;
        }
    }
}
