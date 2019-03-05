using EyeBoard.Logic.Models;
using Microsoft.AspNet.Identity;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EyeBoard.Areas.Admin.Models.Identity
{
    public class RoleStore : IRoleStore<Role, int>,
        IQueryableRoleStore<Role, int>
    {
        public readonly ISession session;

        public RoleStore(ISession session)
        {
            this.session = session;
        }

        #region IQueryableRoleStore<Role, int>
        public IQueryable<Role> Roles
        {
            get
            {
                var query = session.Query<Role>().OrderBy(x => x.Name);
                return query.AsQueryable();
            }
        }
        #endregion

        #region IRoleStore<Role, int>
        public Task CreateAsync(Role role)
        {
            return Task.Run(() => session.SaveOrUpdate(role));
        }

        public Task DeleteAsync(Role role)
        {
            return Task.Run(() => session.Delete(role));
        }

        public void Dispose()
        {
            
        }

        public Task<Role> FindByIdAsync(int roleId)
        {
            return Task.Run(() => session.Get<Role>(roleId));
        }

        public Task<Role> FindByNameAsync(string roleName)
        {
            return Task.Run(() =>
            {
                return session.QueryOver<Role>()
                    .Where(r => r.Name == roleName)
                    .SingleOrDefault();
            });
        }

        public Task UpdateAsync(Role role)
        {
            return Task.Run(() => session.SaveOrUpdate(role));
        }
        #endregion
    }
}