using System.Linq.Expressions;

namespace Plugin.Repository.Repositories
{
    public interface IBaseRepository : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;

        Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class;

        Task EditAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;
        Task EditRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class;

        Task DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;
        Task DeleteAsync<TEntity>(int id, CancellationToken cancellationToken = default) where TEntity : class;
        Task DeleteRangeAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) where TEntity : class;
        Task DeleteRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class;

        IQueryable<TEntity> GetAll<TEntity>() where TEntity : class;

        IQueryable<TEntity> GetAll<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class;

        Task<TEntity> GetByIdAsync<TEntity>(int id) where TEntity : class;

        Task<bool> AnyAsync<TEntity>(int id) where TEntity : class;

        IQueryable<TEntity> FindBy<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;

        Task<TEntity> FirstAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;

        Task<TEntity> FirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate = null) where TEntity : class;

        Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
    }
}
