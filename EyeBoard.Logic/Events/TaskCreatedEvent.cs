using EyeBoard.Logic.Models;
using Profilan.SharedKernel;
using System;

namespace EyeBoard.Logic.Events
{
    public class TaskCreatedEvent : IDomainEvent
    {
        public TaskCreatedEvent(Task task) : this()
        {
            Task = task;
        }

        public TaskCreatedEvent()
        {
            this.Id = Guid.NewGuid();
            DateTimeEventOccurred = DateTime.Now;
        }


        public Guid Id { get; private set; }
        public DateTime DateTimeEventOccurred { get; private set; }

        public Task Task { get; private set; }
    }
}
