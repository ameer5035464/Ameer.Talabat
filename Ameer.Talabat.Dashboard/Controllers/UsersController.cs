using Ameer.Talabat.Core.Domain.Entities.Identity;
using Ameer.Talabat.Dashboard.View_Models.RolesVms;
using Ameer.Talabat.Dashboard.View_Models.UsersVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Ameer.Talabat.Dashboard.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var getUser = await _userManager.Users.ToListAsync();

            var users = new List<UserVM>();

            foreach (var user in getUser)
            {
                users.Add(new UserVM
                {
                    Email = user.Email!,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber!,
                    Id = user.Id,
                    Roles = await _userManager.GetRolesAsync(user)
                });

            }

            return View(users);

        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var mapList = new List<string>();

            foreach (var item in roles)
            {
                mapList.Add(item.Name!);
            }

            if (mapList is not null)
            {
                ViewBag.MapList = mapList;
            }


            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserVM model, List<string> selectedRoles)
        {

            var roles = await _roleManager.Roles.ToListAsync();
            var mapList = new List<string>();

            foreach (var item in roles)
            {
                mapList.Add(item.Name!);
            }

            if (mapList is not null)
            {
                ViewBag.MapList = mapList;
            }

            if (!ModelState.IsValid)
            {

                return View(model);
            }
            else
            {
                if (model is not null)
                {
                    var isExist = await _userManager.FindByEmailAsync(model.Email);

                    if (isExist != null)
                    {
                        ModelState.AddModelError("", "Email already Exist!");
                    }
                    else
                    {
                        var mapUser = new ApplicationUser
                        {
                            Email = model.Email,
                            UserName = model.Email,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            PhoneNumber = model.PhoneNumber

                        };

                        var newUSer = await _userManager.CreateAsync(mapUser, model.Password);

                        if (newUSer.Succeeded)
                        {
                            if (selectedRoles is not null)
                            {
                                foreach (var role in selectedRoles)
                                {
                                    await _userManager.AddToRoleAsync(mapUser, role);
                                }
                            }
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Something Get wrong");
                        }
                    }
                }
                return View(model);
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {

            var isExist = await _userManager.FindByIdAsync(id);
            var allRoles = await _roleManager.Roles.ToListAsync();
            var listIt = new List<RoleVM>();

            if (isExist != null)
            {
                foreach (var item in allRoles)
                {
                    listIt.Add(new RoleVM
                    {
                        Id = item.Id,
                        Name = item.Name!,
                        IsSelected = await _userManager.IsInRoleAsync(isExist, item.Name!)
                    });
                }

                var user = new EditUserVM
                {
                    Id = isExist.Id,
                    Email = isExist.Email!,
                    FirstName = isExist.FirstName,
                    LastName = isExist.LastName,
                    PhoneNumber = isExist.PhoneNumber!,
                    Roles = listIt
                };


                return View(user);
            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserVM model)
        {


            var isExist = await _userManager.FindByIdAsync(model.Id);

            if (isExist != null)
            {
                isExist.Email = model.Email;
                isExist.FirstName = model.FirstName;
                isExist.LastName = model.LastName;
                isExist.PhoneNumber = model.PhoneNumber;
                isExist.UserName = model.Email;

                foreach (var item in model.Roles!)
                {
                    if (item.IsSelected && !await _userManager.IsInRoleAsync(isExist, item.Name))
                    {
                        await _userManager.AddToRoleAsync(isExist, item.Name);
                    }
                    if (!item.IsSelected && await _userManager.IsInRoleAsync(isExist, item.Name))
                    {
                        await _userManager.RemoveFromRoleAsync(isExist, item.Name);
                    }
                }

                await _userManager.UpdateAsync(isExist);

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "No user with this Id");
            return View(model);

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {

            var user = await _userManager.FindByIdAsync(id);
            

            if (user != null)
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);

                if (currentUser!.Id == id)
                {
                    TempData["AlertMessage"] = "you are trying to delete the account thats currently logged in!";
                }
                else if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    TempData["AlertMessage"] = "Unable to delete User with Admin  Role";
                }
                else
                {
                    await _userManager.DeleteAsync(user);
                    TempData["AlertMessage"] = "User deleted Succefuly";
                }
            }
            return RedirectToAction("Index");
        }
    }
}
