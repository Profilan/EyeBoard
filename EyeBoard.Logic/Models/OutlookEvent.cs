using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeBoard.Logic.Models
{
    public class OutlookEvent
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public bool IsAllDay { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
