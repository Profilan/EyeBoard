using Profilan.SharedKernel;
using System;
using System.Collections.Generic;

namespace EyeBoard.Logic.Models
{
    public class Scheduler : Entity<Guid>
    {
        public virtual IList<Task> Tasks { get; set; }

        protected Scheduler() : base()
        {

        }

        public Scheduler(Guid id) : base(id)
        {
            Tasks = new List<Task>();
        }

        
    }
}
