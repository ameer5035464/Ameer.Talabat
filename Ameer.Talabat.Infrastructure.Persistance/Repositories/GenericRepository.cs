using Ameer.Talabat.Core.Domain.Common;
using Ameer.Talabat.Core.Domain.Contracts;
using Ameer.Talabat.Infrastructure.Persistance.Data;

namespace Ameer.Talabat.Infrastructure.Persistance.Repositories
{
    internal class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly StoreContext _storeContext;

        public GenericRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<IEnumerable<TEntity>?> GetAllAsync(bool withNoTraking = false)
        {
            if (!withNoTraking)
                return await _storeContext.Set<TEntity>().AsNoTracking().ToListAsync();

            return await _storeContext.Set<TEntity>().ToListAsync();

        }

        public async Task<TEntity?> GetAsync(TKey id)
        {

            return await _storeContext.Set<TEntity>().FindAsync(id);
        }


        public async Task<IEnumerable<TEntity>?> GetAllSpecAsync(ISpecifications<TEntity, TKey> spec, bool withTraking = false)
        {

            return await SpecificationsEvaluator<TEntity, TKey>.GetQuery(_storeContext.Set<TEntity>(), spec).ToListAsync();
        }

        public async Task<TEntity?> GetSpecAsync(ISpecifications<TEntity, TKey> spec)
        {
            return await SpecificationsEvaluator<TEntity, TKey>.GetQuery(_storeContext.Set<TEntity>(), spec).FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(ISpecifications<TEntity, TKey> spec)
        {
            return await SpecificationsEvaluator<TEntity, TKey>.GetQuery(_storeContext.Set<TEntity>(), spec).CountAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _storeContext.Set<TEntity>().AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _storeContext.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _storeContext.Set<TEntity>().Remove(entity);
        }
    }

}
