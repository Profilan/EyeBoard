using DeEekhoorn.Logic.Models;
using FluentNHibernate.Mapping;

namespace DeEekhoorn.Logic.Mappings
{
    public class DeliveredOrderMap : ClassMap<DeliveredOrder>
    {
        public DeliveredOrderMap()
        {
            Table("EEK_vw_DELIVERED_ORDERS_MAGAZIJN");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Type);
            Map(x => x.Year); //.Column("Jaar");
            Map(x => x.WeekOfYear);
            Map(x => x.DeliveredColli); //.Column("Aantal_Geleverde_Colli");
            
        }
    }
}
