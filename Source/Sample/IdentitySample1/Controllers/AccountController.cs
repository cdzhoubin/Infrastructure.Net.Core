using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentitySample.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentitySample.Areas.Identity.Controllers
{
    //public class AccountController : Controller
    //{
    //    UserManager<ApplicationUser> _usrMgr;
    //    public AccountController(UserManager<ApplicationUser> usrMgr)
    //    {
    //        _usrMgr = usrMgr;
    //    }
    //    public IActionResult Login()
    //    {
    //        return View();
    //    }
    //    public ViewResult Index()
    //    {
    //        return View(_usrMgr.Users);
    //    }
    //    public ViewResult Register()
    //    {
    //        return View();
    //    }
    //    public async Task<IActionResult> Register(CreateUserModel model) {
    //        if (ModelState.IsValid)
    //        {
    //            ApplicationUser user = new ApplicationUser();
    //            user.UserName = model.Name;
    //            user.Email = model.Email;
    //            var resutl = await _usrMgr.CreateAsync(user, model.Password);
    //            if (resutl.Succeeded)
    //            {
    //                return RedirectToAction("index");
    //            } else
    //            {
    //                foreach(IdentityError error in resutl.Errors)
    //                {
    //                    ModelState.AddModelError("", error.Description);
    //                }
    //            }
    //        }
    //        return View(model);
    //    }
    //}
}