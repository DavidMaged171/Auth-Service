using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace Auth.Infrastructure.Persistence.DatabaseContext
{
    public partial class UserDbContext:DbContext
    {
        public IConfiguration _configuration { get; set; }
        public UserDbContext() { }

        public UserDbContext (DbContextOptions<UserDbContext> options):base(options) 
        { }

    }
}
