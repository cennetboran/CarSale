using carpass.be.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using CarSale.Extensions;
using CarSale.Models;

namespace CarSale.Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal AppDbContext _context;
        internal DbSet<TEntity> dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(object id)
        {
            try
            {
                TEntity entity = dbSet.Find(id);
                entity.GetType().GetProperty("Deleted").SetValue(entity, true);
                entity.GetType().GetProperty("UpdatedDate").SetValue(entity, DateTime.UtcNow);
                entity.GetType().GetProperty("DeletedDate").SetValue(entity, DateTime.UtcNow);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ApiResponse<List<TEntity>>> GetAsync(Paging pagination,
            Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet.AsQueryable().AsNoTracking();

            if (filter != null)
                query = query.Where(filter);

            if (includeProperties != null)
                foreach (var includeProperty in includeProperties)
                    query = query.Include(includeProperty);

            return new ApiResponse<List<TEntity>>
            {
                Result = await query
               .Sort(pagination.Column, pagination.SortBy)
               .Skip((pagination.Page - 1) * pagination.Size)
               .Take(pagination.Size)
               .ToDynamicListAsync<TEntity>(),
                Total = query.Count(),
                Message = "Success"
            };
        }

        public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet.AsQueryable().AsNoTracking();

            if (filter != null)
                query = query.Where(filter);

            if (includeProperties != null)
                foreach (var includeProperty in includeProperties)
                    query = query.Include(includeProperty);

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, int id)
        {
            var exist = await _context.Set<TEntity>().FindAsync(id);
            _context.Entry(exist).CurrentValues.SetValues(entity);
            foreach (var property in _context.Entry(entity).Properties)
                if (property.CurrentValue == null)
                    _context.Entry(exist).Property(property.Metadata.Name).IsModified = false;
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
