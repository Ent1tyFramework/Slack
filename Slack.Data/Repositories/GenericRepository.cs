using Slack.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Slack.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private Slack.Data.Contexts.DbContext dbContext;

        public GenericRepository(DbContext dbContext)
        {
            this.dbContext = (Slack.Data.Contexts.DbContext)dbContext;
        }

        public T First()
        {
            using (dbContext = dbContext.GetContext())
            {
                return dbContext.Set<T>().FirstOrDefault();
            }
        }

        public async Task<T> FirstAsync()
        {
            using (dbContext = dbContext.GetContext())
            {
                return await dbContext.Set<T>().FirstOrDefaultAsync();
            }
        }

        public T First(Expression<Func<T, bool>> expression)
        {
            using (dbContext = dbContext.GetContext())
            {
                return dbContext.Set<T>().FirstOrDefault(expression);
            }
        }

        public async Task<T> FirstAsync(Expression<Func<T, bool>> expression)
        {
            using (dbContext = dbContext.GetContext())
            {
                return await dbContext.Set<T>().FirstOrDefaultAsync(expression);
            }
        }

        public T FirstWithIncludes(Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[] includes)
        {
            using (dbContext = dbContext.GetContext())
            {
                return dbContext.Set<T>().Include(includes.First()).FirstOrDefault(expression);
            }
        }

        public async Task<T> FirstWithIncludesAsync(Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[] includes)
        {
            using (dbContext = dbContext.GetContext())
            {
                return await dbContext.Set<T>().Include(includes.First())
                    .FirstOrDefaultAsync(expression);
            }
        }

        public T Last()
        {
            using (dbContext = dbContext.GetContext())
            {
                return dbContext.Set<T>().ToList().Last();
            }
        }

        public async Task<T> LastAsync()
        {
            using (dbContext = dbContext.GetContext())
            {
                return await Task.Run(() => { return dbContext.Set<T>().ToList().Last(); });
            }
        }

        public async Task<IEnumerable<T>> ToListAsync()
        {
            using (dbContext = dbContext.GetContext())
            {
                return await Task.Run(() => { return dbContext.Set<T>().ToList(); });
            }
        }

        public IEnumerable<T> Where(Func<T, bool> func)
        {
            using (dbContext = dbContext.GetContext())
            {
                return dbContext.Set<T>().Where(func).ToList();
            }
        }

        public async Task<IEnumerable<T>> WhereAsync(Func<T, bool> func)
        {
            using (dbContext = dbContext.GetContext())
            {
                return await Task.Run(() => { return dbContext.Set<T>().Where(func).ToList(); });
            }
        }

        public int Count()
        {
            using (dbContext = dbContext.GetContext())
            {
                return dbContext.Set<T>().Count();
            }
        }

        public async Task<bool> AddAsync(T entity)
        {
            try
            {
                using (dbContext = dbContext.GetContext())
                {
                    dbContext.Set<T>().Attach(entity);
                    dbContext.Set<T>().Add(entity);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                using (dbContext = dbContext.GetContext())
                {
                    dbContext.Entry<T>(entity).State = EntityState.Modified;
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> RemoveAsync(T entity)
        {
            try
            {
                using (dbContext = dbContext.GetContext())
                {
                    dbContext.Entry(entity).State = EntityState.Deleted;
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
