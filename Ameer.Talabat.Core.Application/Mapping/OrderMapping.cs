using Ameer.Talabat.Core.Application.Abstraction.Models.Orders;
using Ameer.Talabat.Core.Domain.Entities.Order;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ameer.Talabat.Core.Application.Mapping
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(D => D.DeliveryMethod, O => O.MapFrom(D => D.DeliveryMethod.ShortName))
                .ForMember(D => D.DeliveryMethodCost, O => O.MapFrom(D => D.DeliveryMethod.Cost));
                
            CreateMap<OrderItem, OrderItemDto>();
        }
    }
}
