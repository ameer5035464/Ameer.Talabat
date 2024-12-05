using Ameer.Talabat.Core.Application.Abstraction;
using Ameer.Talabat.Core.Domain.Common;
using Ameer.Talabat.Core.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ameer.Talabat.Infrastructure.Persistance.Data.Interceptors
{
	public class CustomSaveChangesInterceptor : SaveChangesInterceptor
	{
		private readonly ILoggedInUserService _loggedInUserService;

		public CustomSaveChangesInterceptor(ILoggedInUserService loggedInUserService)
        {
			_loggedInUserService = loggedInUserService;
		}

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
		{
			UpdateEntities(eventData.Context);

			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}

		public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
		{
			UpdateEntities(eventData.Context);

			return base.SavedChanges(eventData, result);
		}

		private void UpdateEntities(DbContext? dbContext)
		{
			if (dbContext == null)
				return;

			foreach (var entry in dbContext.ChangeTracker.Entries<BaseEntity<int>>()
				.Where(entity => entity.State is EntityState.Added or EntityState.Modified))
			{
				if (entry.State is EntityState.Added)
				{
					entry.Entity.CreatedBy = _loggedInUserService.UserId!;
					entry.Entity.CreatedOn = DateTime.UtcNow;
				}
				entry.Entity.LastModifiedBy = _loggedInUserService.UserId!;
				entry.Entity.LastModifiedOn = DateTime.UtcNow;
			}


            foreach (var entry in dbContext.ChangeTracker.Entries<Product>()
                .Where(entity => entity.State is EntityState.Added or EntityState.Modified))
            {
               
                    entry.Entity.NormalizedName = entry.Entity.Name.ToUpper();
                
            }

        }

    }
}
