using FinanceBilling.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceBilling.Data.Data
{
    // Main database class that manages database connection and tables
    public class ApplicationDbContext : DbContext
    {
        // Constructor used for Dependency Injection
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Create unique constraints to prevent duplicate
        // usernames and emails in database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Email must be unique
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Username must be unique
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();
        }

        // Database Tables

        // Roles table
        public DbSet<Role> Roles { get; set; }

        // Users table
        public DbSet<User> Users { get; set; }

        // Invoices table
        public DbSet<Invoice> Invoices { get; set; }

        // Payments table
        public DbSet<Payment> Payments { get; set; }
    }
}