using Ameer.Talabat.Core.Application.Abstraction.Services;
using Ameer.Talabat.Core.Domain.Contracts;
using Ameer.Talabat.Infrastructure.Basket_Repositories;
using Ameer.Talabat.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
namespace Ameer.Talabat.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services , IConfiguration configuration)
		{

			services.AddSingleton(typeof(IConnectionMultiplexer),factory =>
			{
				return ConnectionMultiplexer.Connect(configuration.GetConnectionString("redis")!);
			
			});

			services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
			services.AddScoped<IPhotoService, PhotoService>();
			return services;
		}
	}
}
