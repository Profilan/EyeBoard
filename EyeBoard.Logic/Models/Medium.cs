using Profilan.SharedKernel;
using System;
using System.Collections.Generic;

namespace EyeBoard.Logic.Models
{
    public class Medium : Entity<Guid>, ISystemInfo
    {
        public virtual int State { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual int CreatedBy { get; set; }
        public virtual DateTime Modified { get; set; }
        public virtual int ModifiedBy { get; set; }

        public virtual string Title { get; set; }
        public virtual DateTime? PublishUp { get; set; }
        public virtual DateTime? PublishDown { get; set; }
        public virtual int? Ordering { get; set; }
        public virtual string Url { get; set; }
 
        public virtual IList<ScreenGroup> Groups { get; set; }

        // not persisted
        public virtual TrackingState TrackingState { get; set; }

        protected Medium()
        {
            Groups = new List<ScreenGroup>();
        }

        public Medium(Guid id) : base(id)
        {
            Groups = new List<ScreenGroup>();

            PublishUp = DateTime.Now;
            PublishDown = DateTime.MaxValue;
            Ordering = 0;
        }       
    }
}
