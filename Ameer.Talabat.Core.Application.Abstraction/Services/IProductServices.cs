using Ameer.Talabat.Core.Application.Abstraction.Models.ProductDTOs;
using Ameer.Talabat.Core.Application.Abstraction.Pagination;
using Ameer.Talabat.Core.Domain.Entities.Products;
using Ameer.Talabat.Core.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ameer.Talabat.Core.Application.Abstraction.Services
{
    public interface IProductServices
	{
		Task<Pagination<ProductDto>> GetAllProductsAsync(FilterParameter filter);
		Task<ProductDto> GetProductAsync(int id);
		Task<IEnumerable<BrandDto>> GetAllBrands();
		Task<IEnumerable<CategoryDto>> GetAllCategories();

	}
}
