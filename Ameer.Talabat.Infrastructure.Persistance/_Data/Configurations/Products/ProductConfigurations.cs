using Ameer.Talabat.Core.Domain.Entities.Products;
using Ameer.Talabat.Infrastructure.Persistance.Data.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ameer.Talabat.Infrastructure.Persistance.Data.Configurations.Products
{
	internal class ProductConfigurations : BaseEntityConfigurations<Product, int>
	{
		public override void Configure(EntityTypeBuilder<Product> builder)
		{
			base.Configure(builder);

			builder.Property(P => P.Name)
				.IsRequired();
			
			builder.Property(P => P.NormalizedName)
				.IsRequired();

			builder.Property(P => P.Description)
				.IsRequired()
				.HasMaxLength(256);

			builder.Property(P => P.Price)
				.IsRequired()
				.HasColumnType("decimal(9,2)");

			builder.HasOne(D => D.Brand)
				.WithMany()
				.HasForeignKey(D => D.BrandId)
				.OnDelete(DeleteBehavior.SetNull);

			builder.HasOne(D => D.Category)
				.WithMany()
				.HasForeignKey(D => D.CategoryId)
				.OnDelete(DeleteBehavior.SetNull);

		}
	}
}
