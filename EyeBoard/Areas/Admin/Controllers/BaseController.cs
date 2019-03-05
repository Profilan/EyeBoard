using EyeBoard.Helpers;
using EyeBoard.Logic.Models;
using EyeBoard.Areas.Admin.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace EyeBoard.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        private ApplicationUserManager _userManager;

        public BaseController()
        {

        }

        public BaseController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        protected User GetCurrentUser()
        {
            return UserManager.FindByName(User.Identity.Name);
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;

            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null;

            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

            // Modify current threads's culture
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }

        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index");
        }
    }
}