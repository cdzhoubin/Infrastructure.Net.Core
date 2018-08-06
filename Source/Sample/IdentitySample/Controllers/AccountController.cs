using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentitySample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentitySample.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        UserManager<ApplicationUser> _usrMgr;
        SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> usrMgr, SignInManager<ApplicationUser> signInManager)
        {
            _usrMgr = usrMgr;
            _signInManager = signInManager;
        }
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel details,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _usrMgr.FindByEmailAsync(
                    details.Email);
                if(user != null)
                {
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult resutl = await _signInManager.PasswordSignInAsync(
                        user, details.Password,false,false);
                    if (resutl.Succeeded) {
                        return Redirect(returnUrl);
                    } 
                }
                ModelState.AddModelError(nameof(LoginModel.Email),"Invalid user or password ");
            }
            return View(details);
        }
        public ViewResult Index()
        {
            return View(_usrMgr.Users);
        }
        public ViewResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = model.Name;
                user.Email = model.Email;
                var resutl = await _usrMgr.CreateAsync(user, model.Password);
                if (resutl.Succeeded)
                {
                    return RedirectToAction("index");
                }
                else
                {
                    foreach (IdentityError error in resutl.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
    }
}