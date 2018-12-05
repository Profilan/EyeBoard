using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EyeBoard.Logic.Models
{
    public class Weather
    {
        [JsonProperty("coord")]
        public GeoLocation Coord { get; set; }
        [JsonProperty("weather")]
        public IList<WeatherItemInfo> WeatherInfo { get; set; }
        [JsonProperty("base")]
        public string Base { get; set; }
        [JsonProperty("main")]
        public WeatherItemMain Main { get; set; }
        [JsonProperty("win")]
        public WeatherItemWind Wind { get; set; }
        [JsonProperty("clouds")]
        public WeatherItemClouds Clouds { get; set; }
        [JsonProperty("rain")]
        public WeatherItemRain Rain { get; set; }
        [JsonProperty("snow")]
        public WeatherItemSnow Snow { get; set; }
        [JsonProperty("dt")]
        public int Dt { get; set; }
        [JsonProperty("sys")]
        public WeatherItemSys Sys { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("cod")]
        public string Cod { get; set; }
    }

    public class WeatherItemSys
    {
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("sunrise")]
        public string Sunrise { get; set; }
        [JsonProperty("sunset")]
        public string Sunset { get; set; }
    }

    public class Forecast
    {
        [JsonProperty("cod")]
        public string Cod { get; set; } // Internal parameter
        [JsonProperty("message")]
        public string Message { get; set; } // Internal parameter
        [JsonProperty("city")]
        public WeatherCity City { get; set; }
        [JsonProperty("list")]
        public IList<WeatherItem> List { get; set; }
    }

    public class WeatherItem
    {
        [JsonProperty("dt")]
        public int Dt { get; set; } // Time of data forecasted, unix, UTC
        [JsonProperty("main")]
        public WeatherItemMain Main { get; set; }
        [JsonProperty("weather")]
        public IList<WeatherItemInfo> Weather { get; set; }
        [JsonProperty("clouds")]
        public WeatherItemClouds Clouds { get; set; }
        [JsonProperty("wind")]
        public WeatherItemWind Wind { get; set; }
        [JsonProperty("rain")]
        public WeatherItemRain Rain { get; set; }
        [JsonProperty("snow")]
        public WeatherItemSnow Snow { get; set; }
        [JsonProperty("dt_text")]
        public string DtText { get; set; } // Data/time of calculation, UTC
    }

    public class WeatherItemSnow
    {
        [JsonProperty("3h")]
        public float LastThreeHours { get; set; } // Snow volume for last 3 hours, mm
    }

    public class WeatherItemRain
    {
        [JsonProperty("3h")]
        public float LastThreeHours { get; set; } // Rain volume for last 3 hours, mm
    }

    public class WeatherItemWind
    {
        [JsonProperty("speed")]
        public float Speed { get; set; } // Wind speed. Unit Default: meter/sec, Metric: meter/sec, Imperial: miles/hour
        [JsonProperty("deg")]
        public float Deg { get; set; } // Wind direction, degrees (meteorological
    }

    public class WeatherItemClouds
    {
        [JsonProperty("all")]
        public int All { get; set; } // Cloudiness, %
    }

    public class WeatherItemInfo
    {
        [JsonProperty("id")]
        public int Id { get; set; } // Weather condition id
        [JsonProperty("main")]
        public string Main { get; set; } // Group of weather parameters (Rain, Snow, Extreme etc.
        [JsonProperty("description")]
        public string Description { get; set; } // Weather condition within the group
        [JsonProperty("icon")]
        public string Icon { get; set; } // Weaterh icon id
    }

    public class WeatherItemMain
    {
        [JsonProperty("temp")]
        public float Temp { get; set; } // Temperature. Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit
        [JsonProperty("temp_min")]
        public float TempMin { get; set; } // Minimum temperature at the moment of calculation. This is deviation from 'temp' that is possible for large cities and megalopolises geographically expanded (use these parameter optionally). Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.
        [JsonProperty("temp_max")]
        public float TempMax { get; set; } // Maximum temperature at the moment of calculation. This is deviation from 'temp' that is possible for large cities and megalopolises geographically expanded (use these parameter optionally). Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.
        [JsonProperty("pressure")]
        public float Pressure { get; set; } // Atmospheric pressure on the sea level by default, hPa
        [JsonProperty("sea_level")]
        public float SeaLevel { get; set; } //  Atmospheric pressure on the sea level, hPa
        [JsonProperty("grnd_level")]
        public float GrndLevel { get; set; } // Atmospheric pressure on the ground level, hPa
        [JsonProperty("humidity")] 
        public int Humidity { get; set; } // Humidity, %
        [JsonProperty("temp_kf")]
        public float TempKf { get; set; } // Internal parameter

    }

    public class WeatherCity
    {
        [JsonProperty("id")]
        public int Id { get; set; } // City ID
        [JsonProperty("name")]
        public string Name { get; set; } // City name
        [JsonProperty("coord")]
        public GeoLocation Coord { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; } // Country code (GB, NL etc.)
    }
}
