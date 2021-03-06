﻿using EyeBoard.Logic.Events;
using Profilan.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeBoard.Logic.Models
{
    public class Notification : Entity<Guid>, ISystemInfo
    {
        public virtual int State { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime Modified { get; set; }
        public virtual string ModifiedBy { get; set; }

        public virtual string Title { get; set; }
        public virtual DateTime PublishUp { get; set; }
        public virtual DateTime PublishDown { get; set; }
        public virtual int Ordering { get; set; }

        public virtual IList<ScreenGroup> Groups { get; set; }

        // not persisted
        public virtual TrackingState TrackingState { get; set; }

        public Notification()
        {
            Groups = new List<ScreenGroup>();
        }

        public Notification(Guid id) : base(id)
        {
            Groups = new List<ScreenGroup>();

            PublishUp = DateTime.Now;
            PublishDown = DateTime.MaxValue;
            Ordering = 0;
        }

        public static Notification Create(string title)
        {
            Guard.ForNullOrEmpty(title, "title");
            Notification notification = new Notification(Guid.NewGuid())
            {
                Title = title
            };
            return notification;
        }

        public virtual void Update()
        {
            var notificationUpdatedEvent = new NotificationUpdatedEvent(this);

            DomainEvents.Raise(notificationUpdatedEvent);
        }
    }
}
