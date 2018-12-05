using System.ComponentModel.DataAnnotations;

namespace EyeBoard.Models.Identity
{
    public class PasswordViewModel
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Resources))]
        public string Password1 { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password1", ErrorMessage = "The new password and confirmation password do not match.")]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.Resources))]
        public string Password2 { get; set; }
    }
}