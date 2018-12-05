using EyeBoard.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeBoard.Models
{
    public class BoardViewModel
    {
        public ScreenGroup Group { get; set; }
        public string HostName { get; set; }

        public Weather Weather { get; set; }
        public int CurrentTemp { get; set; }
        public string City { get; set; }
        public string Condition { get; set; }
        public int CityId { get; set; }

        public string FeedUrl { get; set; }

        public IEnumerable<Medium> Presentations { get; set; }
    }
}