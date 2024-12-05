using Ameer.Talabat.Core.Application.Abstraction.Models.BasketDTOs;
using Ameer.Talabat.Core.Application.Abstraction.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ameer.Talabat.APIs.Controllers.Controllers.Basket
{
	[Authorize]
	public class BasketController : BaseApiController
	{
		private readonly IServiceManager _serviceManager;

		public BasketController(IServiceManager serviceManager)
		{
			_serviceManager = serviceManager;
		}

		[HttpGet]
		public async Task<ActionResult<CustomerBasketDTO>> GetBasket(string basketId)
		{
			var basket = await _serviceManager.BasketServices.GetCustomBasketAsync(basketId);

			return Ok(basket);
		}

		[HttpPost]
		public async Task<ActionResult<CustomerBasketDTO>> UpdateBasket(CustomerBasketDTO customBasket)
		{
			var basket = await _serviceManager.BasketServices.UpdateCustomBasketAsync(customBasket);
			return Ok(basket);
		}

		[HttpDelete]
		public async Task DeleteBasket(string basketId)
		{
			await _serviceManager.BasketServices.DeleteCustomBasketAsync(basketId);
		}

	}
}
