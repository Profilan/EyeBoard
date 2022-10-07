using DeEekhoorn.Logic.Models;
using NHibernate;
using Profilan.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeEekhoorn.Logic.Repositories
{
    public class FinanceRepository
    {
        public OrderIncome GetOrderIncome()
        {
            using (ISession session = SessionFactory.GetNewSession("db2"))
            {
                var query = from l in session.Query<OrderIncome>()
                            select l;

                var items = query.ToList();

                if (items.Count > 0)
                {
                    return items[0];
                }

                return null;
            }
        }
        public Turnover GetTurnover()
        {
            using (ISession session = SessionFactory.GetNewSession("db2"))
            {
                var query = from l in session.Query<Turnover>()
                            select l;

                var items = query.ToList();

                if (items.Count > 0)
                {
                    return items[0];
                }

                return null;
            }
        }
        public WebshopImport GetWebshopImport()
        {
            using (ISession session = SessionFactory.GetNewSession("db2"))
            {
                var query = from l in session.Query<WebshopImport>()
                            select l;

                var items = query.ToList();

                if (items.Count > 0)
                {
                    return items[0];
                }

                return null;
            }
        }
        public WebshopImportPXL GetWebshopImportPXL()
        {
            using (ISession session = SessionFactory.GetNewSession("db2"))
            {
                var query = from l in session.Query<WebshopImportPXL>()
                            select l;

                var items = query.ToList();

                if (items.Count > 0)
                {
                    return items[0];
                }

                return null;
            }
        }
        public PickedOrder GetPickedOrder()
        {
            using (ISession session = SessionFactory.GetNewSession("db2"))
            {
                var query = from l in session.Query<PickedOrder>()
                            select l;

                var items = query.ToList();

                if (items.Count > 0)
                {
                    return items[0];
                }

                return null;
            }
        }
        public Delivery GetDelivery()
        {
            using (ISession session = SessionFactory.GetNewSession("db2"))
            {
                var query = from l in session.Query<Delivery>()
                            select l;

                var items = query.ToList();

                if (items.Count > 0)
                {
                    return items[0];
                }

                return null;
            }
        }
    }
}
