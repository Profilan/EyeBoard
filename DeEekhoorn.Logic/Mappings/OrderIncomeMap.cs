using DeEekhoorn.Logic.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeEekhoorn.Logic.Mappings
{
    public class OrderIncomeMap : ClassMap<OrderIncome>
    {
        public OrderIncomeMap()
        {
            Table("EEK_vw_NarrowCast_OrderIncome");

            Id(x => x.TotalToday);

            Map(x => x.TotalToday1W).Column("`TotalToday(-1W)`");
            Map(x => x.TotalToday1Y).Column("`TotalToday(-1Y)`");
            Map(x => x.TotalThisWeek);
            Map(x => x.TotalThisWeek1W).Column("`TotalThisWeek(-1W)`");
            Map(x => x.TotalThisWeek1Y).Column("`TotalThisWeek(-1Y)`");
        }
    }
}
