using System.Threading;
using System.Threading.Tasks;
using NHibernate.Event;

namespace Profilan.SharedKernel
{
    internal class EventListener :
        IPostInsertEventListener,
        IPostDeleteEventListener,
        IPostUpdateEventListener,
        IPostCollectionUpdateEventListener
    {
        public void OnPostDelete(PostDeleteEvent ev)
        {
            DispatchEvents(ev.Entity as Entity);
        }

        public Task OnPostDeleteAsync(PostDeleteEvent ev, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public void OnPostInsert(PostInsertEvent ev)
        {
            DispatchEvents(ev.Entity as Entity);
        }

        public Task OnPostInsertAsync(PostInsertEvent ev, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public void OnPostUpdate(PostUpdateEvent ev)
        {
            DispatchEvents(ev.Entity as Entity);
        }

        public Task OnPostUpdateAsync(PostUpdateEvent ev, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public void OnPostUpdateCollection(PostCollectionUpdateEvent ev)
        {
            DispatchEvents(ev.AffectedOwnerOrNull as Entity);
        }

        public Task OnPostUpdateCollectionAsync(PostCollectionUpdateEvent ev, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        private void DispatchEvents(Entity entity)
        {
            if (entity == null)
                return;

            foreach (IDomainEvent domainEvent in entity.DomainEvents)
            {
                DomainEvents.Dispatch(domainEvent);
            }

            entity.ClearEvents();
        }
    }
}
