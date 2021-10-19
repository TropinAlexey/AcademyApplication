using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApplication.Data.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetNotTrackedAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetMany();
        IQueryable<T> GetMany(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(int id);
        Task SaveAsync();
    }
}