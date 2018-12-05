using EyeBoard.Logic.Events;
using Profilan.SharedKernel;
using System;

namespace EyeBoard.Logic.Models
{
    public abstract class AdminModel : Entity
    {
        public virtual int State { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual int CreatedBy { get; set; }
        public virtual DateTime Modified { get; set; }
        public virtual int ModifiedBy { get; set; }

        public AdminModel() : base()
        {
            State = 0;
            Created = DateTime.Now;

            var itemChangedEvent = new ItemChangedEvent(this);
            AddDomainEvent(itemChangedEvent);
        }
    }
}
