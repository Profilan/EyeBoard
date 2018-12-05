using EyeBoard.Logic.Models;
using NHibernate;
using Profilan.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EyeBoard.Logic.Repositories
{
    public class UserRepository : IRepository<User, int>
    {
        public void Delete(int id)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var item = session.Load<User>(id);

                using (ITransaction transaction = session.BeginTransaction())
                {

                    session.Delete(item);
                    transaction.Commit();
                }
            }
        }

        public User GetById(int id)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var item = session.Get<User>(id);

                return item;
            }
        }

        public User GetByUsername(string username)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = from u in session.Query<User>()
                            select u;

                query = query.Where(u => u.UserName == username);

                var users = query.ToList();

                if (users.Count > 0)
                {
                    return query.ToList().Last();
                }
                else
                {
                    return null;
                }
            }
        }

        public void Insert(User entity)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {

                    session.Save(entity);
                    transaction.Commit();
                }
            }
        }

        public IEnumerable<User> List()
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = session.Query<User>().OrderBy(x => x.UserName);


                return query.ToList();
            }
        }

        public IEnumerable<User> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {

                    session.SaveOrUpdate(entity);
                    transaction.Commit();
                }
            }
        }
    }
}
