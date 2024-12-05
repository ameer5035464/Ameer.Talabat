using Ameer.Talabat.Core.Domain.Entities.Products;

namespace Ameer.Talabat.Core.Domain.Contracts
{
	public interface IUnitOfWork : IAsyncDisposable
	{

		IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
		 where TEntity : BaseEntity<TKey>
		 where TKey : IEquatable<TKey>;


		Task<int> CompletedAsync();

	}
}
