using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Username is too long.")]
        public string UserName { get; set; }

        [StringLength(maximumLength: 250, MinimumLength = 20, ErrorMessage = "Invalid entry.")]
        public string Address { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 14, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 14 characters.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password don't match.")]
        public string PasswordConfirm { get; set; }
    }
}
