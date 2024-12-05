using Ameer.Talabat.Core.Domain.Contracts;
using System.Linq.Expressions;

namespace Ameer.Talabat.Core.Domain.Specifications
{
	public class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		public Expression<Func<TEntity, bool>>? Criteria { get; set; }
		public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new();
		public Expression<Func<TEntity, object>>? OrderBy { get; set; }
		public Expression<Func<TEntity, object>>? OrderByDesc { get; set; }
		public int Skip { get; set; }
		public int Take { get; set; }
		public bool IsPaginationEnabled { get; set; }

	}
}
