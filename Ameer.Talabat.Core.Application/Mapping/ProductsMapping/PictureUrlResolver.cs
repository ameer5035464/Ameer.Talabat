using Ameer.Talabat.Core.Application.Abstraction.Models.ProductDTOs;
using Ameer.Talabat.Core.Domain.Entities.Products;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace Ameer.Talabat.Core.Application.Mapping.ProductsMapping
{
    public class PictureUrlResolver : IValueResolver<Product, ProductDto, string?>
    {
        private readonly IConfiguration _configuration;

        public PictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductDto destination, string? destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["Urls:APiBaseUrl"]}/{source.PictureUrl}";
            }
            return string.Empty;
        }
    }
}
