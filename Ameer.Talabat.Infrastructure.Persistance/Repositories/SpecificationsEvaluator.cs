using Ameer.Talabat.Core.Domain.Common;
using Ameer.Talabat.Core.Domain.Contracts;

namespace Ameer.Talabat.Infrastructure.Persistance.Repositories
{
	public static class SpecificationsEvaluator<TEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> db, ISpecifications<TEntity, TKey> spec)
		{
			var baseQuery = db;


			if (spec.Criteria is not null)
				baseQuery = baseQuery.Where(spec.Criteria); // Set<Product>().Where(P => P.Id.Equals(id))

			if (spec.OrderByDesc is not null)
				baseQuery = baseQuery.OrderByDescending(spec.OrderByDesc);

			else if (spec.OrderBy is not null)
				baseQuery = baseQuery.OrderBy(spec.OrderBy);

			if (spec.IsPaginationEnabled)
			{
				baseQuery = baseQuery.Skip(spec.Skip).Take(spec.Take);
			}

			baseQuery = spec.Includes.Aggregate(baseQuery, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));

			return baseQuery;
		}
	}
}
