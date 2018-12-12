using Profilan.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeEekhoorn.Logic.Models
{
    public class DeliveredPackage : Entity<int>
    {
        public virtual int Year { get; set; }
        public virtual int WeekOfYear { get; set; }
        public virtual int ElementGereed { get; set; }
    }
}
