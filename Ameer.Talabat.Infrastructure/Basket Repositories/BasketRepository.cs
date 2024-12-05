using Ameer.Talabat.Core.Domain.Contracts;
using Ameer.Talabat.Core.Domain.Entities.Basket;
using StackExchange.Redis;
using System.Text.Json;

namespace Ameer.Talabat.Infrastructure.Basket_Repositories
{
	public class BasketRepository : IBasketRepository
	{
		private readonly IDatabase _redis;

		public BasketRepository(IConnectionMultiplexer redis)
		{
			_redis = redis.GetDatabase();
		}

		public async Task<CustomerBasket?> GetAsync(string id)
		{
			var basket = await _redis.StringGetAsync(id);

			if (!basket.IsNullOrEmpty)
			{
				return JsonSerializer.Deserialize<CustomerBasket>(basket!);
			}

			return null;
		}

		public async Task<CustomerBasket?> UpdateAsync(CustomerBasket customBasket)
		{
			var customBasketInJson = JsonSerializer.Serialize(customBasket);

			var basket = await _redis.StringSetAsync(customBasket.Id, customBasketInJson, TimeSpan.FromDays(15));

			if (basket == true)
			{
				return customBasket;
			}
			return null;
		}

		public async Task<bool> DeleteAsync(string id)
		{
			return await _redis.KeyDeleteAsync(id);
		}

	}
}
