
using EyeBoard.Logic.Models;
using Microsoft.AspNet.Identity;

namespace EyeBoard.Areas.Admin.Models.Identity
{
    public class ApplicationRoleManager : RoleManager<Role, int>
    {
        public ApplicationRoleManager(IRoleStore<Role, int> store)
            : base(store)
        {
           
        }
    }
}