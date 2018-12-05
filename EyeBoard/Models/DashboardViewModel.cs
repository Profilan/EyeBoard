using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeBoard.Models
{
    public class DashboardViewModel
    {
        public IEnumerable<ScreenViewModel> Screens { get; set; }
    }
}