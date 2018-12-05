using EyeBoard.Logic.Models;
using NHibernate;
using Profilan.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EyeBoard.Logic.Repositories
{
    public class ScreenRepository : IRepository<Screen, Guid>
    {
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Screen GetById(Guid id)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var item = session.Get<Screen>(id);
                    NHibernateUtil.Initialize(item.Group);
                    return item;
                }
            }
        }

        public void Insert(Screen entity)
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

        public IEnumerable<Screen> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Screen> List()
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = from l in session.Query<Screen>()
                            select l;

                query = query.OrderBy(l => l.Title);

                return query.ToList();
            }
        }

        public Screen GetByHostName(string hostName)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var screen = session.QueryOver<Screen>()
                    .Where(s => s.HostName == hostName)
                    .SingleOrDefault();
                NHibernateUtil.Initialize(screen.Group);
                NHibernateUtil.Initialize(screen.Group.Media);

                return screen;

            }
        }

        public void Update(Screen entity)
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
