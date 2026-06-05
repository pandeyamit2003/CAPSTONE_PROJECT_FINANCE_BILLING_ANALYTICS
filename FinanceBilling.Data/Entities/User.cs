namespace FinanceBilling.Data.Entities
{
    // User table
    // Entity class representing the Users table in the database
    public class User
    {
        // Primary Key of the User table
        public int UserId { get; set; }

        // Username used for login
        public string UserName { get; set; }

        // User email address
        public string Email { get; set; }

        // Stores the user's password (currently stored as PasswordHash)
        public string PasswordHash { get; set; }

        // Foreign Key referencing the Roles table
        public int RoleId { get; set; }

        // Navigation property
        // Represents the role assigned to the user
        public Role Role { get; set; }

        // Stores temporary OTP sent to user's email
        public string? OTP { get; set; }

        // Expiry time of OTP (e.g., 10 minutes)
        public DateTime? OTPExpiry { get; set; }

        // Temporary token generated after OTP verification
        public string? ResetToken { get; set; }

        // Expiry time of reset token (e.g., 15 minutes)
        public DateTime? ResetTokenExpiry { get; set; }
    }
}