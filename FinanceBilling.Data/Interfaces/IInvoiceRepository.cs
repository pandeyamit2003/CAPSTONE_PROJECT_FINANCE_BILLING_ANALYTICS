using FinanceBilling.Data.Entities;

namespace FinanceBilling.Data.Interfaces
{
    // Invoice Repository Interface
    // Defines the contract for Invoice database operations
    public interface IInvoiceRepository
    {
        // Retrieves all invoices from the database
        Task<List<Invoice>> GetAllAsync();

        // Retrieves a single invoice by its Id
        Task<Invoice?> GetByIdAsync(int id);

        // Adds a new invoice to the database
        Task AddAsync(Invoice invoice);

        // Updates an existing invoice
        Task UpdateAsync(Invoice invoice);

        // Deletes an invoice using its Id
        Task DeleteAsync(int id);
    }
}