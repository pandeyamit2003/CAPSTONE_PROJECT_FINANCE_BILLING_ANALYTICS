namespace FinanceBilling.Data.DTOs
{
    public class ResetPasswordDto
    {
        // Reset token generated after OTP verification
        public string Token { get; set; }

        // New password entered by user
        public string NewPassword { get; set; }
    }
}