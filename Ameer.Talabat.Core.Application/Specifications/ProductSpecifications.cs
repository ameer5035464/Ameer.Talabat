using Ameer.Talabat.Core.Application.Abstraction.Models.ProductDTOs;
using Ameer.Talabat.Core.Domain.Entities.Products;
using Ameer.Talabat.Core.Domain.Specifications;

namespace Ameer.Talabat.Core.Application.Specifications
{
	public class ProductSpecifications : BaseSpecifications<Product, int>
	{
        public ProductSpecifications()
        {
            Includes.Add(B => B.Brand!);
            Includes.Add(B => B.Category!);
        }

        public ProductSpecifications(FilterParameter filter)
		{	
			if (!string.IsNullOrEmpty(filter.Search))
			{

				if (filter.BrandId != null && filter.CategoryId == null)
					Criteria = P => P.BrandId == filter.BrandId && P.NormalizedName.Contains(filter.Search);
				else if (filter.BrandId == null && filter.CategoryId != null)
					Criteria = P => P.CategoryId == filter.CategoryId && P.NormalizedName.Contains(filter.Search);
				else if (filter.BrandId != null && filter.CategoryId != null)
					Criteria = P => P.BrandId == filter.BrandId && P.CategoryId == filter.CategoryId && P.NormalizedName.Contains(filter.Search);
				else
					Criteria = P => P.NormalizedName.Contains(filter.Search);

			}
			else
			{
				if (filter.BrandId != null && filter.CategoryId == null)
					Criteria = P => P.BrandId == filter.BrandId;
				else if (filter.BrandId == null && filter.CategoryId != null)
					Criteria = P => P.CategoryId == filter.CategoryId;
				else if (filter.BrandId != null && filter.CategoryId != null)
					Criteria = P => P.BrandId == filter.BrandId && P.CategoryId == filter.CategoryId;
			}

			Includes.Add(B => B.Brand!);
			Includes.Add(B => B.Category!);

			if (filter.Sort == "Sort_Asc")
				OrderBy = P => P.Price;
			else if (filter.Sort == "Sort_Desc")
				OrderByDesc = P => P.Price;
			else
				OrderBy = P => P.Id;

			ApplyPagination((filter.PageIndex - 1) * filter.PageSize, filter.PageSize);
		}

		private void ApplyPagination(int skip, int take)
		{
			IsPaginationEnabled = true;
			Skip = skip;
			Take = take;
		}
	}
}
