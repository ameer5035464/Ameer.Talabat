using Ameer.Talabat.Core.Domain.Contracts;

namespace Ameer.Talabat.APIs.Extensions
{
	public static class IntializerExtentions
	{
		public async static Task<WebApplication> IntializeDB(this WebApplication app)
		{
			using var scope = app.Services.CreateAsyncScope();
			var provider = scope.ServiceProvider;
			var dbContext = provider.GetRequiredService<IStoreContextIntializer>();
			var IdentitydbContext = provider.GetRequiredService<IStoreIdentityDbIntializer>();

			try
			{
				await dbContext.IntializeAsync();
				await dbContext.SeedAsync();

				await IdentitydbContext.IntializeAsync();
				await IdentitydbContext.SeedAsync();
			}
			catch (Exception e)
			{
				e.ToString();
			}

			return app;
		}
	}
}
