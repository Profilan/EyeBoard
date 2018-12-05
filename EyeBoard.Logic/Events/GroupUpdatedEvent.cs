
using System;
using EyeBoard.Logic.Models;
using Profilan.SharedKernel;

namespace EyeBoard.Logic.Events
{
    public class GroupUpdatedEvent : IDomainEvent
    {
        public GroupUpdatedEvent(ScreenGroup group)
            : this()
        {
           GroupUpdated = group;
        }
        public GroupUpdatedEvent()
        {
            this.Id = Guid.NewGuid();
            DateTimeEventOccurred = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public DateTime DateTimeEventOccurred { get; private set; }
        public ScreenGroup GroupUpdated { get; private set; }
    }
}
