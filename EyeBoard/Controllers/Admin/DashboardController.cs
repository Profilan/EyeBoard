using EyeBoard.Helpers;
using EyeBoard.Logic.Repositories;
using EyeBoard.Models;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;

namespace EyeBoard.Controllers.Admin
{
    public class DashboardController : BaseController
    {
        private readonly ScreenRepository _screenRepository = new ScreenRepository();

        [Authorize]
        public ActionResult Index()
        {
            var screenItems = _screenRepository.List();
            var screens = new List<ScreenViewModel>();
            foreach (var screen in screenItems)
            {
                screens.Add(new ScreenViewModel()
                {
                    Id = screen.Id,
                    HostName = screen.HostName,
                    IsReachable = IsHostReachable(screen.HostName)
                });
            }

            var dashboardViewModel = new DashboardViewModel()
            {
                Screens = screens
            };

            return View(dashboardViewModel);
        }

        private bool IsHostReachable(string hostname)
        {

            try
            {
                Ping ping = new Ping();
                PingReply pingReply = ping.Send(hostname);

                if (pingReply.Status == IPStatus.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }
    }
}
