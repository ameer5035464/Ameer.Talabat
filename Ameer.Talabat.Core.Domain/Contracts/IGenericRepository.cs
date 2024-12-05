using Ameer.Talabat.Core.Domain.Specifications;

namespace Ameer.Talabat.Core.Domain.Contracts
{
	public interface IGenericRepository<TEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		Task<IEnumerable<TEntity>?> GetAllAsync(bool withTraking = false);
		Task<IEnumerable<TEntity>?> GetAllSpecAsync(ISpecifications<TEntity, TKey> spec,bool withTraking = false);
		Task<int> CountAsync(ISpecifications<TEntity, TKey> spec);
		Task<TEntity?> GetAsync(TKey id);
		Task<TEntity?> GetSpecAsync(ISpecifications<TEntity, TKey> spec);

		Task AddAsync(TEntity entity);

		void Update(TEntity entity);

		void Delete(TEntity entity);
	}
}
