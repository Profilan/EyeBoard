using Microsoft.AspNet.SignalR;

namespace Narrowcast.Service.Hubs
{
    public class TaskSchedulerHub : Hub
    {
        public void RunTask(string id)
        {
            Clients.All.runTask(id);
        }
    }
}
