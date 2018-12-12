using DeEekhoorn.Logic.Models;
using NHibernate;
using NHibernate.Criterion;
using Profilan.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeEekhoorn.Logic.Repositories
{
    public class DeliveredOrderRepository : IRepository<DeliveredOrder, int>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public DeliveredOrder GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(DeliveredOrder entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DeliveredOrder> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DeliveredOrder> List()
        {
            using (ISession session = SessionFactory.GetNewSession("db2"))
            {
                var query = from l in session.Query<DeliveredOrder>()
                            select l;

                return query.ToList();
            }
        }

        public int GetTotalDeliveredColli()
        {
            using (ISession session = SessionFactory.GetNewSession("db2"))
            {
                DeliveredOrder deliveredOrder = null;
                var total = session.QueryOver<DeliveredOrder>(() => deliveredOrder)
                    .Select(Projections.Sum<DeliveredOrder>(x => x.DeliveredColli))
                    
                    .UnderlyingCriteria.UniqueResult();

                return (int)total;
            }
        }

        public void Update(DeliveredOrder entity)
        {
            throw new NotImplementedException();
        }
    }
}
