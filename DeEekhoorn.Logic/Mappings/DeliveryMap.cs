using DeEekhoorn.Logic.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeEekhoorn.Logic.Mappings
{
    public class DeliveryMap : ClassMap<Delivery>
    {
        public DeliveryMap()
        {
            Table("EEK_vw_NarrowCast_Deliveries");

            Id(x => x.PakkettenGeleverd);

            Map(x => x.OrdersGeleverd);
        }
    }
}
