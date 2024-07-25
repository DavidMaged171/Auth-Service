
using Auth.Core.Entities;
using Auth.Infrastructure.Persistence.DatabaseContext;
using Auth.Infrastructure.Persistence.Interfaces;

namespace Auth.Infrastructure.Persistence.Repositories
{
    public class SiteUserRepository : GenericRepository<User>, ISiteUserRepository
    {
        public SiteUserRepository(UserDbContext userDbContext) : base(userDbContext)
        {
        }
    }
}
