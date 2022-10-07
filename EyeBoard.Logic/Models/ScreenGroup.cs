using EyeBoard.Logic.Events;
using Profilan.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EyeBoard.Logic.Models
{
    public class ScreenGroup : Entity<Guid>, ISystemInfo
    {
        public virtual int State { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime Modified { get; set; }
        public virtual string ModifiedBy { get; set; }

        public virtual string Title { get; set; }
        public virtual string Theme { get; set; }

        public virtual IList<Screen> Screens { get; set; }
        public virtual IList<Medium> Media { get; set; }
        public virtual IList<Notification> Notifications { get; set; }

        public ScreenGroup() : base()
        {
            Screens = new List<Screen>();
            Media = new List<Medium>();
            Notifications = new List<Notification>();
        }

        public ScreenGroup(Guid id) : base(id)
        {
            Screens = new List<Screen>();
            Media = new List<Medium>();
            Notifications = new List<Notification>();
            // MarkConflictingAppointments();

            DomainEvents.Register<GroupUpdatedEvent>(Handle);
        }

        public static ScreenGroup Create(string title)
        {
            Guard.ForNullOrEmpty(title, "title");
            ScreenGroup screenGroup = new ScreenGroup(Guid.NewGuid());
            screenGroup.Title = title;
            return screenGroup;
        }

        public virtual Medium AddNewPresentation(Medium presentation)
        {
            if (Media.Any(a => a.Id == presentation.Id))
            {
                throw new ArgumentException("Cannot add duplicate presentation to group.", "presentation");
            }

            presentation.TrackingState = TrackingState.Added;
            Media.Add(presentation);

            // MarkConflictingAppointments();

            var groupUpdatedEvent = new GroupUpdatedEvent(this);
            DomainEvents.Raise(groupUpdatedEvent);

            return presentation;
        }


        public virtual void DeletePresentation(Medium presentation)
        {
            // mark the presentation for deletion by the repository
            var presentationToDelete = this.Media.Where(a => a.Id == presentation.Id).FirstOrDefault();
            if (presentationToDelete != null)
            {
                presentationToDelete.TrackingState = TrackingState.Deleted;
                Media.Remove(presentation);

                var groupUpdatedEvent = new GroupUpdatedEvent(this);
                DomainEvents.Raise(groupUpdatedEvent);
            }

            // MarkConflictingAppointments();
        }

        public virtual Notification AddNewNotification(Notification notification)
        {
            if (Notifications.Any(a => a.Id == notification.Id))
            {
                throw new ArgumentException("Cannot add duplicate notification to group.", "notification");
            }

            notification.TrackingState = TrackingState.Added;
            Notifications.Add(notification);

            // MarkConflictingAppointments();

            var groupUpdatedEvent = new GroupUpdatedEvent(this);
            DomainEvents.Raise(groupUpdatedEvent);

            return notification;
        }


        public virtual void DeleteNotification(Notification notification)
        {
            // mark the presentation for deletion by the repository
            var notificationToDelete = this.Notifications.Where(a => a.Id == notification.Id).FirstOrDefault();
            if (notificationToDelete != null)
            {
                notificationToDelete.TrackingState = TrackingState.Deleted;
                Notifications.Remove(notification);

                var groupUpdatedEvent = new GroupUpdatedEvent(this);
                DomainEvents.Raise(groupUpdatedEvent);
            }

            // MarkConflictingAppointments();
        }

        public virtual void Handle(GroupUpdatedEvent args)
        {
            // MarkConflictingAppointments();
        }
    }
}
