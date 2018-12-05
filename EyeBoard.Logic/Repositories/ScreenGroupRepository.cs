using EyeBoard.Logic.Models;
using NHibernate;
using NHibernate.Linq;
using Profilan.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeBoard.Logic.Repositories
{
    public class ScreenGroupRepository : IRepository<ScreenGroup, Guid>
    {
        public void Delete(Guid id)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var item = session.Load<ScreenGroup>(id);

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(item);
                    transaction.Commit();
                }
            }
        }

        public ScreenGroup GetById(Guid id)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var item = session.Get<ScreenGroup>(id);
                    NHibernateUtil.Initialize(item.Media);
                    NHibernateUtil.Initialize(item.Screens);
                    NHibernateUtil.Initialize(item.Notifications);

                    return item;
                }
            }
        }

        public void Insert(ScreenGroup entity)
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

        public IEnumerable<ScreenGroup> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ScreenGroup> List()
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = session.Query<ScreenGroup>()
                    .FetchMany(x => x.Media)
                    .ToFuture()
                    .OrderBy(x => x.Title);

                session.Query<ScreenGroup>()
                    .FetchMany(x => x.Notifications)
                    .ToFuture();

                return query.ToList();

            }
        }

        public void Update(ScreenGroup entity)
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
