using System.ComponentModel.DataAnnotations;

namespace FinanceBilling.MVC.Models
{
    // ViewModel used for entering new password
    public class ResetPasswordViewModel
    {
        // Reset token generated after OTP verification
        public string Token { get; set; }

        // New password entered by user
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6,ErrorMessage = "Password must contain at least 6 characters")]
        public string NewPassword { get; set; }
    }
}