using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeEekhoorn.Logic.Models
{
    public class PickedOrder
    {
        public virtual int PickedOrdersToday { get; set; }              // Used as primary key (NHibernate conventions)
        public virtual int PickedPackagesToday { get; set; }
        public virtual decimal PickedOrdersValueToday { get; set; }
    }
}
