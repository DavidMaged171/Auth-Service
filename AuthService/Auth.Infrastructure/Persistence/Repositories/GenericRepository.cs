using Auth.Infrastructure.Persistence.DatabaseContext;
using Auth.Infrastructure.Persistence.Interfaces;
using System.Linq.Expressions;

namespace Auth.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly UserDbContext _userDbContext;
        public GenericRepository(UserDbContext userDbContext) 
        {
            _userDbContext = userDbContext;
        }
        public void AddRecord(T entity)
        {
            _userDbContext.Add(entity);
            _userDbContext.SaveChanges();
        }

        public T FindByPrimaryKey<P>(P value)
        {
            return _userDbContext.Set<T>().Find(value);
        }

        public IEnumerable<T> FindWhere(Expression<Func<T,bool>> expression)
        {
            return _userDbContext.Set<T>().Where(expression);
        }

        public IEnumerable<T> GetAll()
        {
            return _userDbContext.Set<T>().ToList();
        }
    }
}
