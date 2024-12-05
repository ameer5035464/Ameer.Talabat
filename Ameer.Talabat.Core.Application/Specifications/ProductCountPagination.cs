using Ameer.Talabat.Core.Domain.Entities.Products;
using Ameer.Talabat.Core.Domain.Specifications;

namespace Ameer.Talabat.Core.Application.Specifications
{
	public class ProductCountPagination : BaseSpecifications<Product, int>
	{
		public ProductCountPagination(int? BrandId, int? CategoryId)
		{
			if (BrandId != null && CategoryId == null)
				Criteria = P => P.BrandId == BrandId;
			else if (BrandId == null && CategoryId != null)
				Criteria = P => P.CategoryId == CategoryId;
			else if (BrandId != null && CategoryId != null)
				Criteria = P => P.BrandId == BrandId && P.CategoryId == CategoryId;
		}
	}
}
