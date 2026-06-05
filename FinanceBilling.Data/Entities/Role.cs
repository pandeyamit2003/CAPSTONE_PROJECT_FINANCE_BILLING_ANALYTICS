namespace FinanceBilling.Data.Entities
{
    // Role table
    // Entity class representing the Roles table in the database
    public class Role
    {
        // Primary Key
        // Unique identifier for each role
        public int RoleId { get; set; }

        // Admin, Accountant, Viewer
        // Name of the role assigned to users
        public string RoleName { get; set; }

        // One role can have many users
        // Navigation property representing the relationship with Users table
        public ICollection<User> Users { get; set; }
            = new List<User>();
    }
}