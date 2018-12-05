using EyeBoard.Logic.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace EyeBoard.Models.Identity
{
    public class ApplicationSignInManager : SignInManager<User, int>
    {
        public ApplicationSignInManager(UserManager<User, int> userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager) { }

        public void SignOut()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        
    }
}