using EyeBoard.Logic.Repositories;
using EyeBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EyeBoard.Controllers
{
    public class HomeController : Controller
    {
        private readonly WeatherRepository _weatherRepository = new WeatherRepository();
        private readonly ScreenRepository _screenRepository = new ScreenRepository();
        private readonly MediaRepository _mediaRepository = new MediaRepository();
        private readonly ScreenGroupRepository _screenGroupRepository = new ScreenGroupRepository();

        [AllowAnonymous]
        public ActionResult Index()
        {
            string IP = Request.UserHostName;
            var hostName = DetermineCompName(IP);

            /*
            var weather = _weatherRepository.GetWeatherInfo(2744819);

            var weatherViewModel = new BoardViewModel()
            {
                WeatherInfo = weather.WeatherInfo[0],
                CurrentTemp = (int)Math.Round(weather.Main.Temp, 0),
                City = weather.Name
            };
            */



            if (!String.IsNullOrEmpty(hostName))
            {
                var screen = _screenRepository.GetByHostName(hostName);

                var viewModel = new BoardViewModel();
                viewModel.ScreenId = screen.Id;
                viewModel.RefreshHours = screen.RefreshTime.Hours;
                viewModel.RefreshMinutes = screen.RefreshTime.Minutes;
                viewModel.RefreshSeconds = screen.RefreshTime.Seconds;
                if (screen.Group != null)
                {
                    viewModel.Presentations = screen.Group.Media.Where(x => x.GetType().Name == "Presentation" || x.GetType().Name == "Movie");
                    viewModel.Group = screen.Group;
                    viewModel.FeedUrl = Server.UrlEncode("http://www.nu.nl/rss/Algemeen");
                    viewModel.CityId = 2744819;

                }

                return View(viewModel);

            }

            return RedirectToAction("index", "dashboard");
        }

        private string DetermineCompName(string IP)
        {
            IPAddress myIP = IPAddress.Parse(IP);
            IPHostEntry GetIPHost = Dns.GetHostEntry(myIP);
            List<string> compName = GetIPHost.HostName.ToString().Split('.').ToList();
            return compName.First();
        }
    }
}