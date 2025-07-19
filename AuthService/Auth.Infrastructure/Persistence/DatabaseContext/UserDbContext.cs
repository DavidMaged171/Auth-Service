using Auth.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace Auth.Infrastructure.Persistence.DatabaseContext
{
    public partial class UserDbContext:IdentityDbContext<ApplicationUser>
    {
        public IConfiguration _configuration { get; set; }
        public UserDbContext() { }

        public UserDbContext (DbContextOptions<UserDbContext> options):base(options) 
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                _configuration = configurationBuilder.Build();
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("UserDB"));
            }
        }
    }
}
