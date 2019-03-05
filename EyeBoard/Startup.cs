using EyeBoard.Areas.Admin.Models.Identity;
using Profilan.SharedKernel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(EyeBoard.Startup))]
namespace EyeBoard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new ApplicationUserManager(new UserStore(SessionFactory.GetNewSession("db1"))));
            app.CreatePerOwinContext(() => new ApplicationRoleManager(new RoleStore(SessionFactory.GetNewSession("db1"))));
            app.CreatePerOwinContext((IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context) => new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Admin/Account/Login"),
                Provider = new CookieAuthenticationProvider()
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            app.MapSignalR();
        }
    }
}
