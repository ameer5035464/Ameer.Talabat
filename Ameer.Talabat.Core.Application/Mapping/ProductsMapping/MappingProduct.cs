using Ameer.Talabat.Core.Application.Abstraction.Models.ProductDTOs;
using Ameer.Talabat.Core.Domain.Entities.Products;
using AutoMapper;

namespace Ameer.Talabat.Core.Application.Mapping.ProductsMapping
{
    public class MappingProduct : Profile
    {

        public MappingProduct()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(D => D.Brand, S => S.MapFrom(F => F.Brand!.Name))
                .ForMember(D => D.Category, S => S.MapFrom(F => F.Category!.Name));


            
            CreateMap<ProductBrand, BrandDto>();

            CreateMap<ProductCategory, CategoryDto>();
        }

    }
}
