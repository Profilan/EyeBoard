
using EyeBoard.Logic.Models;
using EyeBoard.Areas.Admin.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;

namespace EyeBoard.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = SignInManager.PasswordSignIn(model.UserName, model.Password, false, false);
                if (result == SignInStatus.Success)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User() { UserName = model.UserName };
                var result = UserManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    SignInManager.SignIn(user, false, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            SignInManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ApplicationSignInManager SignInManager
        {
            get { return HttpContext.GetOwinContext().Get<ApplicationSignInManager>(); }
        }
        public ApplicationUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<Models.Identity.ApplicationUserManager>(); }
        }
    }
}