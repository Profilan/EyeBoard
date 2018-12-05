using EyeBoard.Logic.Models;
using NHibernate;
using Profilan.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EyeBoard.Logic.Repositories
{
    public class NotificationRepository : IRepository<Notification, Guid>
    {
        public void Delete(Guid id)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var item = session.Load<Notification>(id);

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(item);
                    transaction.Commit();
                }
            }
        }

        public Notification GetById(Guid id)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var item = session.Get<Notification>(id);
                NHibernateUtil.Initialize(item.Groups);

                return item;
            }
        }

        public void Insert(Notification entity)
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

        public IEnumerable<Notification> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Notification> List()
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = session.Query<Notification>().OrderBy(x => x.Ordering);
                return query.ToList();
            }
        }

        public IEnumerable<Notification> ListByGroup(ScreenGroup group)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = from l in session.Query<Notification>()
                            select l;

                query = query.OrderBy(l => l.Title);
                query = query.Where(l => l.Groups.Contains(group));

                return query.ToList();

            }
        }

        public void Update(Notification entity)
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
