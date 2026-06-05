using System.ComponentModel.DataAnnotations;

namespace FinanceBilling.MVC.Models
{
    // ViewModel used to collect user's email
    // for Forgot Password process
    public class ForgotPasswordViewModel
    {
        // Registered email address
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter valid email")]
        public string Email { get; set; }

    }
}