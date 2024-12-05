using Ameer.Talabat.Core.Domain.Entities.Order;

namespace Ameer.Talabat.Core.Application.Abstraction.Models.Orders
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; } = null!;
        public DateTimeOffset OrderDate { get; set; }
        public string Status { get; set; }

        public OrderAddress Address { get; set; } = null!;

        public string DeliveryMethod { get; set; } = null!;
        public decimal DeliveryMethodCost { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; } = new HashSet<OrderItemDto>();
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string PaymentIntentId { get; set; } = string.Empty;


    }
}
