using FinanceBilling.Data.Entities;
using FinanceBilling.Data.Interfaces;

namespace FinanceBilling.Data.Services
{
    // Service class for Invoice operations
    // Implements IInvoiceService interface
    public class InvoiceService : IInvoiceService
    {
        // Repository dependency for Invoice data access
        private readonly IInvoiceRepository _repository;

        // Constructor Dependency Injection
        public InvoiceService(
            IInvoiceRepository repository)
        {
            _repository = repository;
        }

        // Get all invoices
        public async Task<List<Invoice>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        // Get invoice by Id
        public async Task<Invoice?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        // Add a new invoice
        public async Task AddAsync(Invoice invoice)
        {
            await _repository.AddAsync(invoice);
        }

        // Update an existing invoice
        public async Task UpdateAsync(Invoice invoice)
        {
            await _repository.UpdateAsync(invoice);
        }

        // Delete an invoice by Id
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}