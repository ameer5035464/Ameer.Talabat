using Ameer.Talabat.Core.Application.Abstraction.Models.BasketDTOs;
using Ameer.Talabat.Core.Application.Abstraction.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ameer.Talabat.APIs.Controllers.Controllers.Payment
{
    [Authorize]
    public class PaymentController : BaseApiController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("PaymentIntentCreateOrUpdate")]
        public async Task<ActionResult<CustomerBasketDTO>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

            return Ok(basket);
        }
    }
}
