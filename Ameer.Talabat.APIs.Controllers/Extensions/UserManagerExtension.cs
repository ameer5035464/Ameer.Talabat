using Ameer.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ameer.Talabat.APIs.Controllers.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<ApplicationUser> GetCurrentUserWithAddress(this UserManager<ApplicationUser> userManager, ClaimsPrincipal User)
        {

            var user = await userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email == User.FindFirstValue(ClaimTypes.Email));
            return user!;
        }
    }
}
