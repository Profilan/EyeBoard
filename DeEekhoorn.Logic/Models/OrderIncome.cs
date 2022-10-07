using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeEekhoorn.Logic.Models
{
    public class OrderIncome
    {
        public virtual decimal TotalToday { get; set; }         // Used as primary key (NHibernate conventions)
        public virtual decimal TotalToday1W { get; set; }
        public virtual decimal TotalToday1Y { get; set; }
        public virtual decimal TotalThisWeek { get; set; }
        public virtual decimal TotalThisWeek1W { get; set; }
        public virtual decimal TotalThisWeek1Y { get; set; }
    }
}
