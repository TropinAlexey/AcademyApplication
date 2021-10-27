using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> DbSetEntity;

        protected BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            DbSetEntity = _context.Set<T>();
        }

        public virtual Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return DbSetEntity.AnyAsync(predicate);
        }

        public virtual Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return DbSetEntity.FirstOrDefaultAsync(predicate);
        }

        public virtual Task<T> GetNotTrackedAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null) 
                return DbSetEntity.AsNoTracking().FirstOrDefaultAsync();
            return DbSetEntity.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public virtual IQueryable<T> GetMany()
        {
            return DbSetEntity;
        }

        public virtual IQueryable<T> GetMany(Expression<Func<T, bool>> predicate)
        {
            return predicate != null ? DbSetEntity.Where(predicate) : _context.Set<T>();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> DeleteAsync(int id)
        {
            var entity = await DbSetEntity.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException($"Record with id: {id} is not found");

            _context.Entry(entity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"An exception occurred while saving {nameof(T)}: {e.Message}");
            }
        }
    }
}