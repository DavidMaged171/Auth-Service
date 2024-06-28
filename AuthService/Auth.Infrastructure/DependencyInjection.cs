using Auth.Infrastructure.Persistence.DatabaseContext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Auth.Infrastructure.Persistence.Interfaces;
using Auth.Infrastructure.Persistence.Repositories;

namespace Auth.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration) 
        {
            services.AddDbContext<UserDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("Data Source=DESKTOP-TVTRTHB;Initial Catalog=User;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")));

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
