using DeEekhoorn.Logic.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeEekhoorn.Logic.Mappings
{
    public class PickedOrderMap : ClassMap<PickedOrder>
    {
        public PickedOrderMap()
        {
            Table("EEK_vw_NarrowCast_PickedOrders");

            Id(x => x.PickedOrdersToday);

            Map(x => x.PickedPackagesToday);
            Map(x => x.PickedOrdersValueToday);
        }
    }
}
