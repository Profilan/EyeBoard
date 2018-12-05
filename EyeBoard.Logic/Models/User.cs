using Microsoft.AspNet.Identity;
using Profilan.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeBoard.Logic.Models
{
    public class User : Entity<int>, IUser<int>, ISystemInfo
    {
        private readonly EyeBoard.Logic.Helpers.IPasswordHasher _passwordHasher;

        public virtual string UserName { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string HashedPassword { get; set; }
        public virtual bool Enabled { get; set; }

        public virtual int State { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual int CreatedBy { get; set; }
        public virtual DateTime Modified { get; set; }
        public virtual int ModifiedBy { get; set; }

        public virtual IList<Role> Roles { get; set; }

        public User()
        {
            Roles = new List<Role>();
            _passwordHasher = new EyeBoard.Logic.Helpers.PasswordHasher();
        }

        public User(EyeBoard.Logic.Helpers.IPasswordHasher passwordHasher)
        {
            Roles = new List<Role>();
            _passwordHasher = passwordHasher;
        }

        public virtual void SetCredentials(string username, string plainTextPassword)
        {
            UserName = username;
            SetPassword(plainTextPassword);
        }

        public virtual void SetPassword(string plainTextPassword)
        {
            HashedPassword = _passwordHasher.HashPassword(plainTextPassword);
        }

    }
}
