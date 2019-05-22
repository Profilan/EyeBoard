using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeEekhoorn.Logic.Models
{
    public class Delivery
    {
        public virtual int PakkettenGeleverd { get; set; }
        public virtual int OrdersGeleverd { get; set; }
    }
}
