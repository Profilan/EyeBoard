using EyeBoard.Logic.Events;
using Profilan.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EyeBoard.Logic.Models
{
    public class Screen : Entity<Guid>, ISystemInfo
    {
        public virtual int State { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime Modified { get; set; }
        public virtual string ModifiedBy { get; set; }

        public virtual string Title { get; set; }
        public virtual string Location { get; set; }
        public virtual string HostName { get; set; }

        public virtual ScreenGroup Group { get; set; }

        protected Screen()
        {

        }

        public Screen(Guid id) : base(id)
        {
           
        }

        public static Screen Create(string title,
            string location,
            ScreenGroup group)
        {
            Guard.ForNullOrEmpty(title, "title");
            Guard.ForNull(group, "group");
            var screen = new Screen(Guid.NewGuid());
            screen.Title = title;
            screen.Location = location;
            screen.Group = group;
            return screen;
        }

        public virtual void Update()
        {
            var screenUpdatedEvent = new ScreenUpdatedEvent(this);
            DomainEvents.Raise(screenUpdatedEvent);
        }
    }
}
