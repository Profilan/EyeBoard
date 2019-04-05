using EyeBoard.Logic.Repositories;
using Microsoft.AspNet.SignalR;
using System;

namespace APITaskManagement.Service.Hubs
{
    public class TaskSchedulerHub : Hub
    {
        public void RunTask(string id)
        {
            Clients.All.runTask(id);
        }
    }
}
