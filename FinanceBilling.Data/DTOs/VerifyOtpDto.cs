namespace FinanceBilling.Data.DTOs
{
    public class VerifyOtpDto
    {
        // User email
        public string Email { get; set; }

        // OTP entered by user
        public string OTP { get; set; }
    }
}