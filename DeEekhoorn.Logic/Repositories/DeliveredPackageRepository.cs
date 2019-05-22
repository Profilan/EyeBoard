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
    public class DeliveredPackageRepository : IRepository<DeliveredPackage, int>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public DeliveredPackage GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(DeliveredPackage entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DeliveredPackage> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DeliveredPackage> List()
        {
            throw new NotImplementedException();
        }

        public int GetTotalDeliveredPackages()
        {
            using (ISession session = SessionFactory.GetNewSession("db2"))
            {
                DeliveredPackage deliveredPackage = null;
                var total = session.QueryOver<DeliveredPackage>(() => deliveredPackage)
                    .Select(Projections.Sum<DeliveredPackage>(x => x.ElementGereed))

                    .UnderlyingCriteria.UniqueResult();

                return (int)total;
            }
        }

        public int GetTotal()
        {
            using (ISession session = SessionFactory.GetNewSession("db2"))
            {
                var sql = "select PakkettenGeleverd, OrdersGeleverd" +
                          " from EEK_vw_NarrowCast_Deliveries"; ;

                var query = session.CreateSQLQuery(sql);
                object[] result = (object[])query.UniqueResult();


                return Convert.ToInt32(result[0]);
            }
        }


        public void Update(DeliveredPackage entity)
        {
            throw new NotImplementedException();
        }
    }
}
