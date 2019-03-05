using EyeBoard.Logic.Models;
using Profilan.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeBoard.Logic.Events
{
    public class ScreenUpdatedEvent : IDomainEvent
    {
        public ScreenUpdatedEvent(Screen screen)
            : this()
        {
            ScreenUpdated = screen;
        }
        public ScreenUpdatedEvent()
        {
            this.Id = Guid.NewGuid();
            DateTimeEventOccurred = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public DateTime DateTimeEventOccurred { get; private set; }
        public Screen ScreenUpdated { get; private set; }
    }
}
