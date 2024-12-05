using Ameer.Talabat.Core.Domain.Entities.Order;
using Ameer.Talabat.Infrastructure.Persistance.Data.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ameer.Talabat.Infrastructure.Persistance.Data.Configurations.Orders
{
    internal class DeliveryMethodConfigurations : BaseEntityConfigurations<DeliveryMethod,int>
    {
        public override void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            base.Configure(builder);

            builder.Property(D => D.Cost)
                .HasColumnType("decimal(18,2)");

        }
    }
}
