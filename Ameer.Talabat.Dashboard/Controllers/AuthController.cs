using Ameer.Talabat.Core.Domain.Entities.Identity;
using Ameer.Talabat.Dashboard.View_Models.AuthVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ameer.Talabat.Dashboard.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string? ReturnUrl)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string? ReturnUrl, LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "invalid Attempt to login try again!");
                return View(model);
            }

            //var getRole = await _userManager.IsInRoleAsync(user, "Admin");


            var signIn = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);



            if (signIn.Succeeded)
            {
                if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }

                return RedirectToAction("Index", "Users");
            }
            else if (signIn.IsLockedOut)
            {
                ModelState.AddModelError("", "this account is locked!");
            }
            else if (signIn.RequiresTwoFactor)
            {
                ModelState.AddModelError("", "this account require two factor authentication!");
            }
            else if (signIn.IsNotAllowed)
            {
                ModelState.AddModelError("", "this account not verfied yet!");
            }
            else
            {
                ModelState.AddModelError("", "invalid Email or Password");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccesDenied()
        {
            return View();
        }
    }
}
