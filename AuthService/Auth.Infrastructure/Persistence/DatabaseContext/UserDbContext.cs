using Auth.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace Auth.Infrastructure.Persistence.DatabaseContext
{
    public partial class UserDbContext:IdentityDbContext<ApplicationUser,ApplicationRole,int>
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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole
                {
                    Id = 1,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new ApplicationRole
                {
                    Id = 2,
                    Name = "Buyer",
                    NormalizedName = "BUYER",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new ApplicationRole
                {
                    Id = 3,
                    Name = "Seller",
                    NormalizedName = "SELLER",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            );
            base.OnModelCreating(builder);
            builder.Entity<IdentityUserRole<string>>().HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = 1,
                FirstName = "admin",
                LastName = "admin",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@system.com",
                NormalizedEmail = "ADMIN@SYSTEM.COM",
                EmailConfirmed = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Password = "Admin#123",
                IsDeleted = false,
            });

            builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
            {
                UserId = 1,
                RoleId = 1
            });

        }

    }
}
