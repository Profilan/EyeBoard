using EyeBoard.Logic.Repositories;
using EyeBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EyeBoard.Controllers
{
    public class HomeController : Controller
    {
        private readonly WeatherRepository _weatherRepository = new WeatherRepository();

        public async Task<ActionResult> Index()
        {
            var weather = await _weatherRepository.GetWeatherInfo(2744819);

            var viewModel = new BoardViewModel()
            {
                WeatherInfo = weather.WeatherInfo[0],
                CurrentTemp = (int)Math.Round(weather.Main.Temp, 0),
                City = weather.Name
            };

            return View(viewModel);
        }
    }
}