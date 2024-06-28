using Auth.Infrastructure.Persistence.DatabaseContext;
using Auth.Infrastructure.Persistence.Interfaces;

namespace Auth.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private UserDbContext _userDbContext;
        public UnitOfWork(UserDbContext userDBContext) 
        {
            _userDbContext = userDBContext;
        }
    }
}
