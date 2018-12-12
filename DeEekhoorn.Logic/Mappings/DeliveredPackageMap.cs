using DeEekhoorn.Logic.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeEekhoorn.Logic.Mappings
{
    public class DeliveredPackageMap : ClassMap<DeliveredPackage>
    {
        public DeliveredPackageMap()
        {
            Table("EEK_vw_DELIVERED_PACKAGES_INPAKKERIJ");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Year);
            Map(x => x.WeekOfYear);
            Map(x => x.ElementGereed);
        }
    }
}
