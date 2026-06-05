using System.ComponentModel.DataAnnotations;

namespace FinanceBilling.MVC.Models
{
    // ViewModel used for OTP verification
    public class VerifyOtpViewModel
    {
        // User email
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter valid email")]
        public string Email { get; set; }

        // OTP entered by user
        [Required(ErrorMessage = "OTP is required")]
        [StringLength(6,MinimumLength = 6,ErrorMessage = "OTP must be 6 digits")]
        public string OTP { get; set; }
    }
}