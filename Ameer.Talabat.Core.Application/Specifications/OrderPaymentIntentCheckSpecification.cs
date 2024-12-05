using Ameer.Talabat.Core.Domain.Entities.Order;
using Ameer.Talabat.Core.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ameer.Talabat.Core.Application.Specifications
{
    public class OrderPaymentIntentCheckSpecification : BaseSpecifications<Order,int>
    {
        public OrderPaymentIntentCheckSpecification(string paymentIntent)
        {
            Criteria = O => O.PaymentIntentId == paymentIntent;
        }
    }
}
