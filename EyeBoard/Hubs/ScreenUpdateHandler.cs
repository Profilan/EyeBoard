using EyeBoard.Logic.Events;
using Microsoft.AspNet.SignalR;
using Profilan.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeBoard.Hubs
{
    public class ScreenUpdateHandler : IHandle<ScreenUpdatedEvent>
    {
        public void Handle(ScreenUpdatedEvent args)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ScreenHub>();
            hubContext.Clients.All.updateScreen(args.ScreenUpdated.Id);
        }
    }
}