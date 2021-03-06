﻿
using EyeBoard.Logic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EyeBoard.Areas.Admin.Models
{
    public class GroupViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Title", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
              ErrorMessageResourceName = "TitleRequired")]
        [StringLength(255, ErrorMessageResourceType = typeof(Resources.Resources),
              ErrorMessageResourceName = "TitleLong")]
        public string Title { get; set; }

        [Display(Name = "Author", ResourceType = typeof(Resources.Resources))]
        public string CreatedBy { get; set; }

        [Display(Name = "Date", ResourceType = typeof(Resources.Resources))]
        public DateTime Modified { get; set; }

        public string UploadUrl { get; set; }
        public IEnumerable<Medium> Media { get; set; }
        public string AcceptFileTypes { get; set; }
        public int MaxFileSize { get; set; }
        public string UserId { get; set; }

        [Display(Name = "Presentations", ResourceType = typeof(Resources.Resources))]
        public IEnumerable<Medium> Presentations { get; set; }
        public IEnumerable<Medium> SelectedPresentations { get; set; }

        [Display(Name = "Videos", ResourceType = typeof(Resources.Resources))]
        public IEnumerable<Medium> Videos { get; set; }
        public IEnumerable<Medium> SelectedVideos { get; set; }
    }
}