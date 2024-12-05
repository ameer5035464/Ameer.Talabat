using Ameer.Talabat.Core.Domain.Entities.Order;
using Ameer.Talabat.Infrastructure.Persistance.Data.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ameer.Talabat.Infrastructure.Persistance.Data.Configurations.Orders
{
    internal class OrderItemConfigurations : BaseEntityConfigurations<OrderItem, int>
    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            base.Configure(builder);

            builder.Property(O => O.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
