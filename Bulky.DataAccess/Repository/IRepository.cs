using System.Linq.Expressions;

namespace Bulky.DataAccess.Repository;

public interface IRepository<T> where T : class
{
    //T - Category
    IEnumerable<T> GetAll();
    T Get(Expression<Func<T, bool>> filter);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    void RemoveRange(IEnumerable<T> entity);
    
}