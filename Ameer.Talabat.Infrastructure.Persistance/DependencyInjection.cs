using Ameer.Talabat.Core.Application.Abstraction;
using Ameer.Talabat.Core.Domain.Contracts;
using Ameer.Talabat.Core.Domain.Entities.Identity;
using Ameer.Talabat.Infrastructure.Persistance._Identity;
using Ameer.Talabat.Infrastructure.Persistance.Data;
using Ameer.Talabat.Infrastructure.Persistance.Data.Interceptors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ameer.Talabat.Infrastructure.Persistance
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configurations)
		{
			#region Data DBContext
			services.AddDbContext<StoreContext>((options) =>
				{
					options
					.UseLazyLoadingProxies()
					.UseSqlServer(configurations.GetConnectionString("StoreContext"));
				});

			services.AddScoped(typeof(IStoreContextIntializer), typeof(StoreContextIntializer));
			services.AddScoped<CustomSaveChangesInterceptor>();
			services.AddScoped(typeof(ISaveChangesInterceptor), typeof(CustomSaveChangesInterceptor));

			services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));
			#endregion

			#region Identity DBContext

			services.AddDbContext<StoreIdentityDbContext>((options) =>
			{
				options
				.UseLazyLoadingProxies()
				.UseSqlServer(configurations.GetConnectionString("StoreIdentityContext"));
			});


			services.AddScoped(typeof(IStoreIdentityDbIntializer), typeof(StoreIdentityDbIntializer));

			#endregion


			return services;
		}
	}
}
