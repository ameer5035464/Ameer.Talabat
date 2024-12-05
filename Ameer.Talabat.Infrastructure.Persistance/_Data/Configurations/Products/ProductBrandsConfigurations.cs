using Ameer.Talabat.Core.Domain.Entities.Products;
using Ameer.Talabat.Infrastructure.Persistance.Data.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ameer.Talabat.Infrastructure.Persistance.Data.Configurations.Products
{
    internal class ProductBrandsConfigurations : BaseEntityConfigurations<ProductBrand, int>
    {

        public override void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            base.Configure(builder);

            builder.Property(B => B.Name)
                .IsRequired();

            builder.HasIndex(B => B.Name).IsUnique();
        }
    }
}
