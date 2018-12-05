using EyeBoard.Logic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EyeBoard.Logic.Repositories
{
    public class WeatherRepository
    {
        public async Task<Weather> GetWeatherInfo(int cityId)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync("http://api.openweathermap.org/data/2.5/weather?id=" + cityId + "&units=metric&lang=nl&APPID=77bad148323084427283a018dd1a76bc");
            var data = response.Content.ReadAsStringAsync().Result;

            Weather weather = JsonConvert.DeserializeObject<Weather>(data);

            return weather;
        }

        public async Task<Forecast> GetForecastInfo(int cityId)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync("http://api.openweathermap.org/data/2.5/forecast?id=" + cityId + "&units=metric&lang=nl&APPID=77bad148323084427283a018dd1a76bc");
            var data = response.Content.ReadAsStringAsync().Result;

            Forecast forecast = JsonConvert.DeserializeObject<Forecast>(data);

            return forecast;
        }
    }
}
