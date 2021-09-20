using LearningAspDotNetCoreMVC.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningAspDotNetCoreMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login,
            string returnUrl = null)
        {
            if( !ModelState.IsValid )
            {
                return View();
            }

            // Attempt sign-in with the provided email address and password.
            var result = await _signInManager.PasswordSignInAsync(
                login.EmailAddress, login.Password, login.RememberMe, false);

            // If there is an issue return an error to the user.
            if( !result.Succeeded )
            {
                ModelState.AddModelError("", "Login error!");
                return View();
            }

            // If there was no returnUrl provided, return the now signed in
            // user to the home page.
            if( string.IsNullOrWhiteSpace(returnUrl) )
            {
                return RedirectToAction("Index", "Home");
            }

            return Redirect(returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            // Use the provided asynchronous method to sign out the user.
            await _signInManager.SignOutAsync();

            // If there was no returnUrl provided, return the now signed in
            // user to the home page.
            if ( string.IsNullOrWhiteSpace(returnUrl) )
            {
                return RedirectToAction("Index, Home");
            }

            return Redirect(returnUrl);
        }
               
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registration)
        {
            if (!ModelState.IsValid)
            {
                return View(registration);
            }

            var newUser = new IdentityUser
            {
                Email = registration.EmailAddress,
                UserName = registration.EmailAddress,
            };

            var result = await _userManager.CreateAsync(newUser, registration.Password);

            if( !result.Succeeded )
            {
                foreach (var error in result.Errors.Select(x => x.Description))
                {
                    ModelState.AddModelError("", error);
                }
                return View();
            }

            return RedirectToAction("Login");
        }

    }
}
