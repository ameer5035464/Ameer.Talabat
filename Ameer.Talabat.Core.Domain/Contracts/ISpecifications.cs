using System.Linq.Expressions;

namespace Ameer.Talabat.Core.Domain.Contracts
{
	public interface ISpecifications<TEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		public Expression<Func<TEntity, bool>>? Criteria { get; set; }
		//Func<Product,bool> => (P => P.Id.Equals(id))
		public List<Expression<Func<TEntity, object>>> Includes { get; set; }
		//Func<Product,ProductBrand> => (P => P.Brand)
		public Expression<Func<TEntity, object>>? OrderBy { get; set; }
		public Expression<Func<TEntity, object>>? OrderByDesc { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }
    }
}
