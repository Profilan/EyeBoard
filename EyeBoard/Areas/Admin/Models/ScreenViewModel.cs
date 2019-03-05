
using EyeBoard.Logic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EyeBoard.Areas.Admin.Models
{
    public class ScreenViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Title", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
              ErrorMessageResourceName = "TitleRequired")]
        [StringLength(255, ErrorMessageResourceType = typeof(Resources.Resources),
              ErrorMessageResourceName = "TitleLong")]
        public string Title { get; set; }

        [Display(Name = "Location", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
                ErrorMessageResourceName = "LocationRequired")]
        [StringLength(255, ErrorMessageResourceType = typeof(Resources.Resources),
              ErrorMessageResourceName = "LocationLong")]
        public string Location { get; set; }

        [Display(Name = "HostName", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
                ErrorMessageResourceName = "HostNameRequired")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resources),
              ErrorMessageResourceName = "HostNameLong")]
        public string HostName { get; set; }

        [Display(Name = "Author", ResourceType = typeof(Resources.Resources))]
        public string CreatedBy { get; set; }

        [Display(Name = "Date", ResourceType = typeof(Resources.Resources))]
        public DateTime Modified { get; set; }

        [Display(Name = "Group", ResourceType = typeof(Resources.Resources))]
        public string GroupId { get; set; }
        public IEnumerable<SelectListItem> Groups { get; set; }

        public bool IsReachable { get; set; }
    }
}