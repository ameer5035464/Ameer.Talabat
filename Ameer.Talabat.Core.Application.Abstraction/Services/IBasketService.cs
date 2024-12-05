using Ameer.Talabat.Core.Application.Abstraction.Models.BasketDTOs;

namespace Ameer.Talabat.Core.Application.Abstraction.Services
{
	public interface IBasketService
	{
		Task<CustomerBasketDTO> GetCustomBasketAsync(string basketId);
		Task<CustomerBasketDTO> UpdateCustomBasketAsync(CustomerBasketDTO customBasket);
		Task DeleteCustomBasketAsync(string basketId);
	}
}
