using Ameer.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;

namespace Ameer.Talabat.Infrastructure.Persistance._Identity
{
	public class StoreIdentityDbContext : IdentityDbContext<ApplicationUser>
	{
		public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			//builder.ApplyConfiguration(new ApplicationUserConfigurations());

			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), type =>
				type.Namespace!.Contains("Ameer.Talabat.Infrastructure.Persistance._Identity.Configurations")
			);

		}
	}
}
