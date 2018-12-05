using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeBoard.Logic.Models;
using Microsoft.AspNet.SignalR;
using Profilan.SharedKernel;

namespace EyeBoard.Hubs
{
    public class ScreenHub : Hub
    {
        public void UpdateGroup(Guid id)
        {
            Clients.All.updateGroup(id);
        }
    }
}