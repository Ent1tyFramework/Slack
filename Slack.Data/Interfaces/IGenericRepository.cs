using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Slack.Data.Interfaces
{
    public interface IGenericRepository<T>
    {
        T First();
        Task<T> FirstAsync();

        T First(Expression<Func<T, bool>> expression);
        Task<T> FirstAsync(Expression<Func<T, bool>> expression);

        T FirstWithIncludes(Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[] includes);
        Task<T> FirstWithIncludesAsync(Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[] includes);

        T Last();
        Task<T> LastAsync();

        IEnumerable<T> Where(Func<T, bool> func);
        Task<IEnumerable<T>> WhereAsync(Func<T, bool> func);

        int Count();
        Task<IEnumerable<T>> ToListAsync();

        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> RemoveAsync(T entity);
    }
}
