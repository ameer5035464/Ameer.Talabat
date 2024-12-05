using Ameer.Talabat.Core.Application.Abstraction.Models.Orders;
using Ameer.Talabat.Core.Application.Abstraction.Services;
using Ameer.Talabat.Core.Domain.Entities.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ameer.Talabat.APIs.Controllers.Controllers.Order
{
    [Authorize]
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpPost("CreateOrder")]
        public async Task<ActionResult<Core.Domain.Entities.Order.Order>> CreateOrder(string basketId, int deliveryMethodId, [FromBody] OrderAddress address)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var createOrder = await _orderService.CreateOrderAsync(buyerEmail!, basketId, deliveryMethodId, address);

            return Ok(createOrder);
        }

        [HttpGet("GetOrders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var getOrders = await _orderService.GetAllUserOrdersAsync(buyerEmail!);

            return Ok(getOrders);

        }

        [HttpGet("GetOrder")]
        public async Task<ActionResult<OrderDto>> GetOrder(int orderId)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var getOrder = await _orderService.GetOrderByIdAsync(buyerEmail!, orderId);

            return Ok(getOrder);
        }

        [AllowAnonymous]
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethod>>> GetDeliveryMethod()
        {
            var deliveryMethod = await _orderService.GetDeliveryMetodsAsync();
            return Ok(deliveryMethod);
        }

    }
}
