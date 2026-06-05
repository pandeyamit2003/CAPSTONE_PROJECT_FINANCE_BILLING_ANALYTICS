using FinanceBilling.Data.Entities;

namespace FinanceBilling.Data.Interfaces
{
    // Payment Repository Interface
    // Defines the contract for Payment database operations
    public interface IPaymentRepository
    {
        // Retrieves all payments from the database
        Task<List<Payment>> GetAllAsync();

        // Retrieves a specific payment by its Id
        Task<Payment?> GetByIdAsync(int id);

        // Adds a new payment to the database
        Task AddAsync(Payment payment);

        // Updates an existing payment
        Task UpdateAsync(Payment payment);

        // Deletes a payment using its Id
        Task DeleteAsync(int id);
    }
}