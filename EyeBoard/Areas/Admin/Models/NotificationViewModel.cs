using EyeBoard.Logic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EyeBoard.Areas.Admin.Models
{
    public class NotificationViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Title", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
              ErrorMessageResourceName = "TitleRequired")]
        [StringLength(255, ErrorMessageResourceType = typeof(Resources.Resources),
              ErrorMessageResourceName = "TitleLong")]
        public string Title { get; set; }

        public DateTime PublishUp { get; set; }

        public DateTime PublishDown { get; set; }
        [Display(Name = "Groups", ResourceType = typeof(Resources.Resources))]
        public IEnumerable<ScreenGroup> Groups { get; set; }

        public IEnumerable<ScreenGroup> SelectedGroups { get; set; }
    }
}