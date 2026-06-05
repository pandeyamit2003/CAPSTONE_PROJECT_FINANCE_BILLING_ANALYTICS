using FinanceBilling.Data.Entities;
using FinanceBilling.Data.Interfaces;

namespace FinanceBilling.Data.Services
{
    // Service class for Payment operations
    // Implements IPaymentService interface
    public class PaymentService : IPaymentService
    {
        // Repository dependency for Payment data access
        private readonly IPaymentRepository _repository;

        // Constructor Dependency Injection
        public PaymentService(IPaymentRepository repository)
        {
            _repository = repository;
        }

        // Get all payments
        public async Task<List<Payment>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        // Get payment by Id
        public async Task<Payment?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        // Add a new payment
        public async Task AddAsync(Payment payment)
        {
            await _repository.AddAsync(payment);
        }

        // Update an existing payment
        public async Task UpdateAsync(Payment payment)
        {
            await _repository.UpdateAsync(payment);
        }

        // Delete a payment by Id
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}