using Ameer.Talabat.Core.Domain.Entities.Products;
using Ameer.Talabat.Infrastructure.Persistance.Data.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ameer.Talabat.Infrastructure.Persistance.Data.Configurations.Products
{
	internal class ProductCategoryConfigurations:BaseEntityConfigurations<ProductCategory,int>
	{
		public override void Configure(EntityTypeBuilder<ProductCategory> builder)
		{
			base.Configure(builder);

			builder.Property(C => C.Name)
				.IsRequired();

			builder.HasIndex(C => C.Name).IsUnique();
		}
	}
}
