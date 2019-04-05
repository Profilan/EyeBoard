using EyeBoard.Logic.Models;
using NHibernate;
using Profilan.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EyeBoard.Logic.Repositories
{
    public class TaskRepository : IRepository<Task, Guid>
    {
        public void Delete(Guid id)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var item = session.Load<Task>(id);

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(item);
                    transaction.Commit();
                }
            }
        }

        public Task GetById(Guid id)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var item = session.Get<Task>(id);
                    
                    return item;
                }
            }
        }

        public void Insert(Task entity)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(entity);
                    transaction.Commit();
                }
            }
        }

        public IEnumerable<Task> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Task> List()
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = session.Query<Task>();

                return query.ToList();
            }
        }

        public void Update(Task entity)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
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
