using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Profi.Areas.AdminPanel.Models;
using Profi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Profi.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class AccountController : Controller
    {
        private UserManager<AppAdmin> _userManager { get;  }
        private SignInManager<AppAdmin> _signInManager { get; }

        public AccountController(SignInManager<AppAdmin> signInManager, UserManager<AppAdmin> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;

        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM admin)
        {
            Areas.AdminPanel.Models.AppAdmin adminDb = await _userManager.FindByEmailAsync(admin.Email);
            if (adminDb == null)
            {
                ModelState.AddModelError("", "Email or password is not correct.");
                return View(admin);
            }
            Areas.AdminPanel.Models.AppAdmin newAdmin = new Areas.AdminPanel.Models.AppAdmin { 
                Email=admin.Email

            };

            var signinResult = await _signInManager.CheckPasswordSignInAsync(newAdmin, admin.Password, lockoutOnFailure: true);
            if (signinResult.IsLockedOut)
            {
                ModelState.AddModelError("", "Please try a few minutes later.");
                return View(admin);
            }
            if (!signinResult.Succeeded)
            {
                ModelState.AddModelError("", "Email or password is wrong.");
                return View(admin);
            }
            if (!adminDb.isActivated)
            {
                ModelState.AddModelError("", "Please verify your account.");
                return View(admin);
            }
            return RedirectToAction("Index", "Dashboard");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Dashboard");
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
