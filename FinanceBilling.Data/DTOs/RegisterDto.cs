namespace FinanceBilling.Data.DTOs
{
    // DTO used for user registration requests
    public class RegisterDto
    {
        // Username entered by the user during registration
        public string UserName { get; set; }

        // Email address of the user
        public string Email { get; set; }

        // Password entered by the user
        public string Password { get; set; }

        // Role assigned to the user
        public int RoleId { get; set; }
    }
}