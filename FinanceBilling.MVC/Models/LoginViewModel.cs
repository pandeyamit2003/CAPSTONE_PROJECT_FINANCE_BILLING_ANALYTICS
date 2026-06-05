using System.ComponentModel.DataAnnotations;

namespace FinanceBilling.MVC.Models
{
    // ViewModel used for User Login
    public class LoginViewModel
    {
        // Username entered by the user
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        // Password entered by the user
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}