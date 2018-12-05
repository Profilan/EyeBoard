using EyeBoard.Logic.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EyeBoard.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "UserName", ResourceType = typeof(Resources.Resources))]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "DisplayName", ResourceType = typeof(Resources.Resources))]
        public string DisplayName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Resources))]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The new password and confirmation password do not match.")]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.Resources))]
        public string ConfirmPassword { get; set; }

        [Display(Name = "UserRole", ResourceType = typeof(Resources.Resources))]
        public string UserRole { get; set; }
        public IList<Role> Roles { get; set; }

    }
}