using EyeBoard.Logic.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Profilan.SharedKernel;

namespace EyeBoard.Models.Identity
{
    public class ApplicationUserManager : UserManager<User, int>
    {
        public ApplicationUserManager(IUserStore<User, int> store)
            : base(store)
        {
            UserValidator = new UserValidator<User, int>(this);
            PasswordValidator = new PasswordValidator();
            PasswordHasher = new CustomPasswordHasher();
        }
    }
}