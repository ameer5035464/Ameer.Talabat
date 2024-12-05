using System.ComponentModel.DataAnnotations.Schema;

namespace Ameer.Talabat.Core.Domain.Entities.Order
{
    public class Order : BaseEntity<int>
    {
        public Order()
        {

        }
        public Order(string buyerEmail, OrderAddress address, DeliveryMethod deliveryMethod, ICollection<OrderItem> orderItems, decimal subTotal,string paymentIntent)
        {
            BuyerEmail = buyerEmail;
            Address = address;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntent;
        }

        public string BuyerEmail { get; set; } = null!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public OrderAddress Address { get; set; } = null!;

        public virtual DeliveryMethod DeliveryMethod { get; set; } = null!;
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }

        [NotMapped]
        public decimal Total => SubTotal + DeliveryMethod.Cost;
        public string? PaymentIntentId { get; set; }

    }
}
