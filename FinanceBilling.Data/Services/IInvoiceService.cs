using FinanceBilling.Data.Entities;

namespace FinanceBilling.Data.Services
{
    // Invoice Service Interface
    // Defines the contract for invoice business operations
    public interface IInvoiceService
    {
        // Retrieves all invoices
        Task<List<Invoice>> GetAllAsync();

        // Retrieves a specific invoice by Id
        Task<Invoice?> GetByIdAsync(int id);

        // Adds a new invoice
        Task AddAsync(Invoice invoice);

        // Updates an existing invoice
        Task UpdateAsync(Invoice invoice);

        // Deletes an invoice by Id
        Task DeleteAsync(int id);
    }
}