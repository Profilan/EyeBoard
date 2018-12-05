
using EyeBoard.Logic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EyeBoard.Models
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
        public int UserId { get; set; }
    }
}