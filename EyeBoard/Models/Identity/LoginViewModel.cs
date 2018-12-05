using System.ComponentModel.DataAnnotations;

namespace EyeBoard.Models.Identity
{
    public class LoginViewModel
    {
        [Display(Name = "UserName", ResourceType = typeof(Resources.Resources))]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Resources.Resources))]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}