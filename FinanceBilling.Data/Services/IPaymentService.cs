using FinanceBilling.Data.Entities;

namespace FinanceBilling.Data.Services
{
    // Payment Service Interface
    // Defines the contract for payment business operations
    public interface IPaymentService
    {
        // Retrieves all payments
        Task<List<Payment>> GetAllAsync();

        // Retrieves a specific payment by Id
        Task<Payment?> GetByIdAsync(int id);

        // Adds a new payment
        Task AddAsync(Payment payment);

        // Updates an existing payment
        Task UpdateAsync(Payment payment);

        // Deletes a payment by Id
        Task DeleteAsync(int id);
    }
}