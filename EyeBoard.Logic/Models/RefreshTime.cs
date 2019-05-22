using Profilan.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeBoard.Logic.Models
{
    public class RefreshTime : ValueObject<RefreshTime>
    {
        public virtual int Hours { get; set; }
        public virtual int Minutes { get; set; }
        public virtual int Seconds { get; set; }

        private RefreshTime()
        {

        }

        public RefreshTime(int hours, int minutes, int seconds)
        {
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }

        protected override bool EqualsCore(RefreshTime other)
        {
            return (Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds);
        }
    }
}
