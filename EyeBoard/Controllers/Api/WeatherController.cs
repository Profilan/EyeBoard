using EyeBoard.Logic.Repositories;
using EyeBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EyeBoard.Controllers.Api
{
    public class WeatherController : ApiController
    {
        private readonly WeatherRepository _weatherRepository = new WeatherRepository();

        [HttpGet]
        [Route("api/weather/{cityId}")]
        public async Task<IHttpActionResult> GetInfo(int cityId)
        {
            var weather = await _weatherRepository.GetWeatherInfo(cityId);
            

            var weatherViewModel = new BoardViewModel()
            {
                Weather = weather,
                
                CurrentTemp = (int)Math.Round(weather.Main.Temp, 0),
                City = weather.Name
            };

            return Ok(weatherViewModel);
        }
    }
}
