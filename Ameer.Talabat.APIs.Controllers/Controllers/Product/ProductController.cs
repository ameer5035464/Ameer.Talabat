using Ameer.Talabat.Core.Application.Abstraction.Models.ProductDTOs;
using Ameer.Talabat.Core.Application.Abstraction.Pagination;
using Ameer.Talabat.Core.Application.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ameer.Talabat.APIs.Controllers.Controllers.Product
{

    public class ProductController : BaseApiController
    {

        private readonly IServiceManager _serviceManager;

        public ProductController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet] //Api/Product
        public async Task<ActionResult<Pagination<ProductDto>>> GetAllProducts([FromQuery] FilterParameter filter)
        {
            var getProduct = await _serviceManager.ProductServices.GetAllProductsAsync(filter);
            return Ok(getProduct);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {

            var GetProduct = await _serviceManager.ProductServices.GetProductAsync(id);
            return Ok(GetProduct);
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var Brands = await _serviceManager.ProductServices.GetAllBrands();
            return Ok(Brands);
        }

        [HttpGet("Categories")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategories()
        {
            var Categories = await _serviceManager.ProductServices.GetAllCategories();
            return Ok(Categories);
        }
    }
}
