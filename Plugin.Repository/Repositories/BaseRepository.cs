using Microsoft.EntityFrameworkCore;
using Plugin.Repository.DbContexts;
using System.Linq.Expressions;

namespace Plugin.Repository.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        protected readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            if (entity == null)
                throw new ArgumentException("entity");

            await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }



        public virtual async Task DeleteRangeAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) where TEntity : class
        {
            var entities = await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
            if (entities.Any())
            {
                await DeleteRangeAsync(entities);
            }
        }

        public virtual async Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class
        {
            if (entities != null && entities.Any())
            {
                try
                {
                    _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;

                    foreach (var entity in entities)
                    {
                        await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
                    }

                    await _dbContext.SaveChangesAsync(cancellationToken);
                }
                finally
                {
                    _dbContext.ChangeTracker.AutoDetectChangesEnabled = true;
                }
            }
        }

        public virtual async Task EditAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            if (entity == null)
                throw new ArgumentException("entity");

            _dbContext.Set<TEntity>().Attach(entity);

            _dbContext.Entry(entity).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task EditRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class
        {
            if (entities != null && entities.Any())
            {
                try
                {
                    _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;

                    foreach (var entity in entities)
                    {
                        _dbContext.Set<TEntity>().Attach(entity);

                        _dbContext.Entry(entity).State = EntityState.Modified;
                    }

                    await _dbContext.SaveChangesAsync(cancellationToken);
                }
                finally
                {
                    _dbContext.ChangeTracker.AutoDetectChangesEnabled = true;
                }
            }
        }

        public virtual async Task DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            _dbContext.Set<TEntity>().Attach(entity);

            _dbContext.Set<TEntity>().Remove(entity);


            await _dbContext.SaveChangesAsync(cancellationToken);


        }

        public virtual async Task DeleteAsync<TEntity>(int id, CancellationToken cancellationToken = default) where TEntity : class
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);
            if (entity != null)
            {

                _dbContext.Set<TEntity>().Remove(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);

            }
        }

        public virtual async Task DeleteRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class
        {
            if (entities != null && entities.Any())
            {
                try
                {
                    _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;

                    _dbContext.Set<TEntity>().RemoveRange(entities);

                    await _dbContext.SaveChangesAsync(cancellationToken);
                }
                finally
                {
                    _dbContext.ChangeTracker.AutoDetectChangesEnabled = true;
                }
            }
        }

        public virtual IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return _dbContext.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetAll<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            return _dbContext.Set<TEntity>().Where(filter);
        }

        public virtual async Task<TEntity> GetByIdAsync<TEntity>(int id) where TEntity : class
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<bool> AnyAsync<TEntity>(int id) where TEntity : class
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);
            return entity != null;
        }

        public IQueryable<TEntity> FindBy<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            var query = _dbContext.Set<TEntity>().Where(predicate);
            return query;
        }

        public async Task<TEntity> FirstAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            var result = await _dbContext.Set<TEntity>().Where(predicate).FirstAsync();
            return result;
        }

        public async Task<TEntity> FirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate = null) where TEntity : class
        {
            TEntity result;
            if (predicate == null)
            {
                result = await _dbContext.Set<TEntity>().FirstOrDefaultAsync();
            }
            else
            {
                result = await _dbContext.Set<TEntity>().Where(predicate).FirstOrDefaultAsync();
            }
            return result;
        }

        public async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            var result = await _dbContext.Set<TEntity>().Where(predicate).FirstOrDefaultAsync();
            return result != null;
        }

        #region __ IDisposable Support __
        private bool _isDisposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
