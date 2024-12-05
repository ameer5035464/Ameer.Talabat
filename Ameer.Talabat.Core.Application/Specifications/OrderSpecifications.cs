using Ameer.Talabat.Core.Domain.Entities.Order;
using Ameer.Talabat.Core.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ameer.Talabat.Core.Application.Specifications
{
    public class OrderSpecifications : BaseSpecifications<Order,int>
    {
        public OrderSpecifications(string buyerEmail)
        {
            Criteria = O => O.BuyerEmail == buyerEmail; 

            Includes.Add(B => B.DeliveryMethod!);
            Includes.Add(B => B.OrderItems!);
        }
        
        public OrderSpecifications(string buyerEmail , int orderId)
        {
            Criteria = O => O.BuyerEmail == buyerEmail && O.Id == orderId; 

            Includes.Add(B => B.DeliveryMethod!);
            Includes.Add(B => B.OrderItems!);
        }
    }
}
