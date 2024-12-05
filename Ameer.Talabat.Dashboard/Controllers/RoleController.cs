using Ameer.Talabat.Dashboard.View_Models.RolesVms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ameer.Talabat.Dashboard.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet("GetRoles")]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return View(roles);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("CreateRole")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateRole")]
        public async Task<IActionResult> Create([FromForm] CreateRoleVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var isExist = await _roleManager.RoleExistsAsync(model.Name);

            if (isExist)
            {
                ModelState.AddModelError(string.Empty, $"{model.Name} Role already Exist!");
            }

            else
            {
                var role = new IdentityRole
                {
                    Name = model.Name
                };

                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var identityRole = await _roleManager.FindByIdAsync(id);

            if (identityRole == null)
            {
                return View();
            }


            var role = new EditRoleVM
            {
                id = identityRole.Id,
                Name = identityRole.Name!
            };

            return View(role);

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] EditRoleVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {

                var role = await _roleManager.FindByIdAsync(model.id);

                if (role == null)
                {
                    ModelState.AddModelError(string.Empty, "No Role With This ID");
                }

                else
                {
                    role.Name = model.Name;

                    var result = await _roleManager.UpdateAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        foreach (var err in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, err.Description);
                        }
                    }
                }

                return View(model);
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                if (role.Name == "Admin")
                {
                    TempData["AlertMessage"] = "Can't delete Admin Role";
                }
                else
                {
                    await _roleManager.DeleteAsync(role);
                    TempData["AlertMessage"] = "Role deleted Succefuly";
                }

            }
            return RedirectToAction("Index", "Role");

        }


    }
}
