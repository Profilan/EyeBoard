using System.Collections.Generic;
using System.Linq;
using EyeBoard.Logic.Models;
using NHibernate;
using NHibernate.Linq;
using Profilan.SharedKernel;

namespace EyeBoard.Logic.Repositories
{
    public class RoleRepository : IRepository<Role, int>
    {
        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Role GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(Role entity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Role> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Role> List()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Role> ListBySearchstring(string searchstring)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = session.Query<Role>().OrderBy(x => x.Name).Where(x => x.Name.Like("%" + searchstring + "%"));

                return query.ToList();
            }
        }

        public void Update(Role entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
