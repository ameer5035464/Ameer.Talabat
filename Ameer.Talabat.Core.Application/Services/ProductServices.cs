using Ameer.Talabat.Core.Application.Abstraction.Models.ProductDTOs;
using Ameer.Talabat.Core.Application.Abstraction.Pagination;
using Ameer.Talabat.Core.Application.Abstraction.Services;
using Ameer.Talabat.Core.Application.Specifications;
using Ameer.Talabat.Core.Domain.Contracts;
using Ameer.Talabat.Core.Domain.Entities.Products;
using Ameer.Talabat.Core.Domain.Specifications;
using AutoMapper;

namespace Ameer.Talabat.Core.Application.Services
{
    internal class ProductServices : IProductServices
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public ProductServices(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Pagination<ProductDto>> GetAllProductsAsync(FilterParameter filter)
		{
			var specs = new ProductSpecifications(filter);

			var GetProducts = await _unitOfWork.GetRepository<Product, int>().GetAllSpecAsync(specs);
			var MapProducts = _mapper.Map<IEnumerable<ProductDto>>(GetProducts);

			var productCount = await _unitOfWork.GetRepository<Product,int>().GetAllAsync();

			return new Pagination<ProductDto>(filter.PageSize, filter.PageIndex, productCount!.Count(), MapProducts);
		}

		public async Task<ProductDto> GetProductAsync(int id)
		{
			var specs = new BaseSpecifications<Product, int>();

			specs.Criteria = P => P.Id == id;
			specs.Includes.Add(P => P.Brand!);
			specs.Includes.Add(P => P.Category!);

			var GetProduct = await _unitOfWork.GetRepository<Product, int>().GetSpecAsync(specs);
			var MapProduct = _mapper.Map<ProductDto>(GetProduct);

			return MapProduct;
		}

		public async Task<IEnumerable<BrandDto>> GetAllBrands()
		{
			var GetBrands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
			var MapBrands = _mapper.Map<IEnumerable<BrandDto>>(GetBrands);

			return MapBrands;
		}

		public async Task<IEnumerable<CategoryDto>> GetAllCategories()
		{
			var GetCategories = await _unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync();
			var MapCategories = _mapper.Map<IEnumerable<CategoryDto>>(GetCategories);

			return MapCategories;
		}
	}
}
