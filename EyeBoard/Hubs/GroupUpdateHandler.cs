using EyeBoard.Logic.Events;
using Microsoft.AspNet.SignalR;
using Profilan.SharedKernel;

namespace EyeBoard.Hubs
{
    public class GroupUpdateHandler : IHandle<GroupUpdatedEvent>
    {
        public void Handle(GroupUpdatedEvent args)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ScreenHub>();
            hubContext.Clients.All.updateGroup(args.GroupUpdated.Id);
        }
    }
}