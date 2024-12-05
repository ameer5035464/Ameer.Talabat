using Ameer.Talabat.Core.Domain.Entities.Products;
using Ameer.Talabat.Dashboard.View_Models.BrandsVMs;
using Ameer.Talabat.Dashboard.View_Models.CategoryVMs;
using Ameer.Talabat.Dashboard.View_Models.ProductsVMs;
using AutoMapper;

namespace Ameer.Talabat.Dashboard.Mapping
{
    public class ProductMVCMapping : Profile
    {

        public ProductMVCMapping()
        {
            CreateMap<CreateEditProductVM, Product>().ForMember(p => p.NormalizedName, p => p.MapFrom(p => p.Name.ToUpper())).ReverseMap();
            CreateMap<EditProductVM, Product>().ForMember(p => p.NormalizedName, p => p.MapFrom(p => p.Name.ToUpper())).ReverseMap();
            CreateMap<Product, ProductDetailsVM>()
                .ForMember(b => b.Brand, s => s.MapFrom(a => a.Brand!.Name))
                .ForMember(b => b.Category, s => s.MapFrom(a => a.Category!.Name));

            CreateMap<ProductBrand, GetBrandsVM>();
            CreateMap<BrandCreateVm, ProductBrand>();

            CreateMap<ProductCategory, GetCategoriesVm>();
            CreateMap<CreateCategoryVm, ProductCategory>();
        }
    }
}
