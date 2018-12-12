using Profilan.SharedKernel;

namespace DeEekhoorn.Logic.Models
{
    public class DeliveredOrder : Entity<int>
    {
        public virtual string Type { get; set; }
        public virtual int Year { get; set; }
        public virtual int WeekOfYear { get; set; }
        public virtual int DeliveredColli { get; set; }
    }
}
