using EyeBoard.Logic.Models;
using Microsoft.AspNet.Identity;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EyeBoard.Areas.Admin.Models.Identity
{
    public class UserStore : IUserStore<User, int>,
        IUserPasswordStore<User, int>,
        IUserLockoutStore<User, int>,
        IUserTwoFactorStore<User, int>,
        IQueryableUserStore<User, int>,
        IUserRoleStore<User, int>
    {
        private readonly ISession session;

        public UserStore(ISession session)
        {
            this.session = session;
        }

        #region IUserStore<User, int>
        public Task CreateAsync(User user)
        {
            return Task.Run(() => session.SaveOrUpdate(user));
        }

        public Task DeleteAsync(User user)
        {
            return Task.Run(() => session.Delete(user));
        }

        public Task<User> FindByIdAsync(int userId)
        {
            return Task.Run(() => session.Get<User>(userId));
        }

        public Task<User> FindByNameAsync(string userName)
        {
            return Task.Run(() =>
            {
                return session.QueryOver<User>()
                    .Where(u => u.UserName == userName)
                    .SingleOrDefault();
            });
        }

        public Task UpdateAsync(User user)
        {
            return Task.Run(() => session.SaveOrUpdate(user));
        }
        #endregion

        #region IUserPasswordStore<User, int>
        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            return Task.Run(() => user.HashedPassword = passwordHash);
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task.FromResult(user.HashedPassword);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(true);
        }
        #endregion

        #region IUserLockoutStore<User, int>
        public Task<DateTimeOffset> GetLockoutEndDateAsync(User user)
        {
            return Task.FromResult(DateTimeOffset.MaxValue);
        }

        public Task SetLockoutEndDateAsync(User user, DateTimeOffset lockoutEnd)
        {
            return Task.CompletedTask;
        }

        public Task<int> IncrementAccessFailedCountAsync(User user)
        {
            return Task.FromResult(0);
        }

        public Task ResetAccessFailedCountAsync(User user)
        {
            return Task.CompletedTask;
        }

        public Task<int> GetAccessFailedCountAsync(User user)
        {
            return Task.FromResult(0);
        }

        public Task<bool> GetLockoutEnabledAsync(User user)
        {
            return Task.FromResult(false);
        }

        public Task SetLockoutEnabledAsync(User user, bool enabled)
        {
            return Task.CompletedTask;
        }
        #endregion

        #region IUserTwoFactorStore<User, int>
        public Task SetTwoFactorEnabledAsync(User user, bool enabled)
        {
            return Task.CompletedTask;
        }

        public Task<bool> GetTwoFactorEnabledAsync(User user)
        {
            return Task.FromResult(false);
        }
        #endregion

        #region IQueryableUserStore<User, int>
        public IQueryable<User> Users
        {
            get
            {
                var query = session.Query<User>().OrderBy(x => x.UserName);
                return query.AsQueryable();
            }
        }
        #endregion

        public void Dispose()
        {
            //do nothing
        }

        public Task AddToRoleAsync(User user, string roleName)
        {
            return Task.Run(() => {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var role = session.QueryOver<Role>()
                        .Where(r => r.Name == roleName)
                        .SingleOrDefault();

                    if (role != null)
                    {
                        user.Roles.Add(role);
                        session.SaveOrUpdate(user);
                        transaction.Commit();
                    }
                }
            });
        }

        public Task RemoveFromRoleAsync(User user, string roleName)
        {
            return Task.Run(() => {
                var query = from r in session.Query<Role>()
                            select r;

                query = query.Where(r => r.Name == roleName);

                var roles = query.ToList();
                if (roles.Count() > 0)
                {
                    user.Roles.Remove(roles[0]);
                    session.SaveOrUpdate(user);
                }
            });
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            IList<string> roles = new List<string>();
            foreach (var role in user.Roles)
            {
                roles.Add(role.Name);
            }
            return Task.FromResult(roles);
        }

        public Task<bool> IsInRoleAsync(User user, string roleName)
        {
            foreach (var role in user.Roles)
            {
                if (role.Name == roleName)
                {
                    return Task.FromResult(true);
                }
            }

            return Task.FromResult(false);
        }
    }
}