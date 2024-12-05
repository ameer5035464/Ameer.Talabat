using Ameer.Talabat.Core.Domain.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ameer.Talabat.Core.Domain.Contracts
{
	public interface IBasketRepository
	{
		Task<CustomerBasket?> GetAsync(string id);

		Task<CustomerBasket?> UpdateAsync(CustomerBasket customBasket);

		Task<bool> DeleteAsync(string id);
	}
}
