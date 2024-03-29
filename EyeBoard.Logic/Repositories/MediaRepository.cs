﻿using EyeBoard.Logic.Models;
using NHibernate;
using NHibernate.Linq;
using Profilan.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EyeBoard.Logic.Repositories
{
    public class MediaRepository : IRepository<Medium, Guid>
    {
        public void Delete(Guid id)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var item = session.Load<Medium>(id);

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(item);
                    transaction.Commit();
                }
            }
        }

        public Medium GetById(Guid id)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var medium = session.Get<Medium>(id);
                    NHibernateUtil.Initialize(medium.Groups);
                    return medium;
                }
            }
        }

        public IEnumerable<Medium> ListByUser(string userId)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = from l in session.Query<Medium>()
                            select l;

                query = query.OrderBy(l => l.Title);
                query = query.Where(l => l.CreatedBy == userId);

                return query.ToList();

            }
        }

        public IEnumerable<Medium> ListByGroup(ScreenGroup group)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = from l in session.Query<Medium>()
                            select l;

                query = query.OrderBy(l => l.Title);
                query = query.Where(l => l.Groups.Contains(group));

                return query.ToList();

            }
        }

        public void Insert(Medium entity)
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

        public IEnumerable<Medium> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Medium> List()
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = session.Query<Medium>()
                    .FetchMany(x => x.Groups)
                    .ToFuture()
                    .OrderBy(x => x.Title);

                return query.ToList();

            }
        }

        public void Update(Medium entity)
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
