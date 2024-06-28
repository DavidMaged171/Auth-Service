using System.Linq.Expressions;

namespace Auth.Infrastructure.Persistence.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T FindByPrimaryKey<P>(P value);
        IEnumerable<T> FindWhere(Expression<Func<T, bool>> expression);
        void AddRecord(T entity);
    }
}
