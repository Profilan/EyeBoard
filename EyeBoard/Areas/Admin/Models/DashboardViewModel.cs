using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeBoard.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public IEnumerable<ScreenViewModel> Screens { get; set; }
    }
}