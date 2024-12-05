using Ameer.Talabat.Core.Domain.Entities.Order;
using Ameer.Talabat.Infrastructure.Persistance.Data.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ameer.Talabat.Infrastructure.Persistance.Data.Configurations.Orders
{
    internal class OrderConfigurations : BaseEntityConfigurations<Order, int>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);

            builder.Property(O => O.Status)
                .HasConversion(OS => OS.ToString(), Os => (OrderStatus)Enum.Parse(typeof(OrderStatus), Os));

            builder.OwnsOne(O => O.Address);

            builder.HasOne(O => O.DeliveryMethod)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
                

            builder.Property(O => O.SubTotal)
                .HasColumnType("decimal(18,2)");


        }
    }
}
