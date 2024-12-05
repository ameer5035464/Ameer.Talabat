using Ameer.Talabat.Core.Application.Abstraction.Models.Orders;
using Ameer.Talabat.Core.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ameer.Talabat.Core.Application.Abstraction.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string buyerEmail,string basketId,int deliveryMethodId,OrderAddress address);
        Task<IEnumerable<OrderDto?>> GetAllUserOrdersAsync(string buyerEmail);
        Task<OrderDto?> GetOrderByIdAsync(string buyerEmail, int orderId);
        Task<IEnumerable<DeliveryMethod>> GetDeliveryMetodsAsync();
    }
}
