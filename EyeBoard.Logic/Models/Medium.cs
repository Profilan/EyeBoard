using EyeBoard.Logic.Events;
using Profilan.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EyeBoard.Logic.Models
{
    public class Medium : Entity<Guid>, ISystemInfo
    {
        public virtual int State { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime Modified { get; set; }
        public virtual string ModifiedBy { get; set; }

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
        
        public virtual ScreenGroup AddNewGroup(ScreenGroup group)
        {
            if (Groups.Any(a => a.Id == group.Id))
            {
                throw new ArgumentException("Cannot add duplicate group to medium.", "group");
            }

            TrackingState = TrackingState.Added;
            Groups.Add(group);

            var groupUpdatedEvent = new GroupUpdatedEvent(group);
            DomainEvents.Raise(groupUpdatedEvent);

            return group;
        }

        public virtual void DeleteGroup(ScreenGroup group)
        {
            var groupToDelete = this.Groups.Where(a => a.Id == group.Id);
            if (groupToDelete != null)
            {
                TrackingState = TrackingState.Deleted;
                Groups.Remove(group);

                var groupUpdatedEvent = new GroupUpdatedEvent(group);
                DomainEvents.Raise(groupUpdatedEvent);
            }
        }
    }
}
