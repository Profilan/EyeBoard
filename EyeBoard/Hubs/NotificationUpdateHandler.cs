using EyeBoard.Logic.Events;
using EyeBoard.Logic.Models;
using Microsoft.AspNet.SignalR;
using Profilan.SharedKernel;

namespace EyeBoard.Hubs
{
    public class NotificationUpdateHandler : IHandle<NotificationUpdatedEvent>
    {
        public void Handle(NotificationUpdatedEvent args)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ScreenHub>();
            foreach (ScreenGroup group in args.NotificationUpdated.Groups)
            {
                hubContext.Clients.All.updateGroup(args.NotificationUpdated.Id);
            }
            
        }
    }
}