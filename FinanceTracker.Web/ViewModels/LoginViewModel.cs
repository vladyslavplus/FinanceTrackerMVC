using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username/Email is required.")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "The {0} must be at {2} and at max {1} characters long.")]
        public string Password { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
