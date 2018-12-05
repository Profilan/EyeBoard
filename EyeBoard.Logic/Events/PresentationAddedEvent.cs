
using System;
using EyeBoard.Logic.Models;
using Profilan.SharedKernel;

namespace EyeBoard.Logic.Events
{
    public class PresentationAddedEvent : IDomainEvent
    {
        public PresentationAddedEvent(Medium presentation)
            : this()
        {
            PresentationAdded = presentation;
        }
        public PresentationAddedEvent()
        {
            this.Id = Guid.NewGuid();
            DateTimeEventOccurred = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public DateTime DateTimeEventOccurred { get; private set; }
        public Medium PresentationAdded { get; private set; }
    }
}
