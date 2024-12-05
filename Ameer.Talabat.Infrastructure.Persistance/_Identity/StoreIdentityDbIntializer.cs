using Ameer.Talabat.Core.Domain.Contracts;
using Ameer.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Ameer.Talabat.Infrastructure.Persistance._Identity
{
    public class StoreIdentityDbIntializer : IStoreIdentityDbIntializer
    {
        private readonly StoreIdentityDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public StoreIdentityDbIntializer(
            StoreIdentityDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task IntializeAsync()
        {
            var check = await _dbContext.Database.GetPendingMigrationsAsync();

            if (check.Any())
            {
                await _dbContext.Database.MigrateAsync();
            }
        }

        public async Task SeedAsync()
        {
            if (!await _userManager.Users.AnyAsync())
            {
                var user = new ApplicationUser
                {
                    FirstName = "Ameer",
                    LastName = "Mohamed",
                    Email = "ameerelnagdi503564@gmail.com",
                    UserName = "ameerelnagdi503564@gmail.com",
                    PhoneNumber = "01095410698"
                };

                await _userManager.CreateAsync(user, "Ameer@1999");

            }

            if (!await _roleManager.Roles.AnyAsync())
            {
                var role = new IdentityRole
                {
                    Name = "Admin"
                };
                await _roleManager.CreateAsync(role);
            }
        }
    }
}
