using Ameer.Talabat.Core.Domain.Common;
using Ameer.Talabat.Core.Domain.Contracts;
using Ameer.Talabat.Infrastructure.Persistance.Data;
using Ameer.Talabat.Infrastructure.Persistance.Repositories;
using System.Collections.Concurrent;

namespace Ameer.Talabat.Infrastructure.Persistance.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly StoreContext _storeContext;

		private readonly ConcurrentDictionary<string, object> _Repo;

		public UnitOfWork(StoreContext storeContext)
		{
			_storeContext = storeContext;

			_Repo = new ConcurrentDictionary<string, object>();
		}


		public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
			where TEntity : BaseEntity<TKey>
			where TKey : IEquatable<TKey>
		{

			var KeyIs = typeof(TEntity).Name;
			var NewObject = new GenericRepository<TEntity, TKey>(_storeContext);

			return (IGenericRepository<TEntity, TKey>)_Repo.GetOrAdd(KeyIs, NewObject);

		}

		public async Task<int> CompletedAsync()
		{
			return await _storeContext.SaveChangesAsync();
		}

		public ValueTask DisposeAsync()
		{
			return _storeContext.DisposeAsync();
		}

	}
}
