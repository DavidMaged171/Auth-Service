using Auth.Applicatoin.BusinessInterfaces;
using Auth.Applicatoin.BusinessLogic;
using Auth.Applicatoin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Applicatoin
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAuthProcessor, AuthProcessor>();

            services.AddIdentity<ApplicationUser, IdentityRole>();
            return services;
        }
    }
}
