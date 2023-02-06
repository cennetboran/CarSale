using System.Linq.Expressions;
using CarSale.Models;

namespace CarSale.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<ApiResponse<List<TEntity>>> GetAsync(Paging pagination,
           Expression<Func<TEntity, bool>> filter = null,
          params Expression<Func<TEntity, object>>[] includeProperties);

        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
          params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, int id);
        Task<bool> DeleteAsync(object id);

    }
}
