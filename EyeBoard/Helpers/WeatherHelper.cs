using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeBoard.Helpers
{
    public static class WeatherHelper
    {
        public static string Icon(string code)
        {
            return "https://openweathermap.org/img/w/" + code + ".png";
        }
    }
}