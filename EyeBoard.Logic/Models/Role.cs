
using Microsoft.AspNet.Identity;
using Profilan.SharedKernel;
using System.Collections.Generic;

namespace EyeBoard.Logic.Models
{
    public class Role : Entity<int>, IRole<int>
    {
        public virtual string Name { get; set; }

        public virtual IList<User> Users { get; set; }

        public Role()
        {
            Users = new List<User>();
        }
    }
}
