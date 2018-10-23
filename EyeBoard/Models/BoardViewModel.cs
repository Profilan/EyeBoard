using EyeBoard.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeBoard.Models
{
    public class BoardViewModel
    {
        public WeatherItemInfo WeatherInfo { get; set; }
        public int CurrentTemp { get; set; }
        public string City { get; set; }
        public string Condition { get; set; }
    }
}