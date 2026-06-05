namespace FinanceBilling.Data.DTOs
{
    // DTO used for user login requests
    public class LoginDto
    {
        // Username entered by the user during login
        public string UserName { get; set; }

        // Password entered by the user during login
        public string Password { get; set; }
    }
}