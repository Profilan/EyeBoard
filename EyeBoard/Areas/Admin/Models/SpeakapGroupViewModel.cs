using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EyeBoard.Areas.Admin.Models
{
    public class SpeakapGroupViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Naam")]
        public string Name { get; set; }

        [Display(Name = "Omschrijving")]
        public string Description { get; set; }

        [Display(Name = "Zichtbaar")]
        public bool Enabled { get; set; }
    }
}