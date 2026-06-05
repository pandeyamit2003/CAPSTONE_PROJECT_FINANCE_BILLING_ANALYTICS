using FinanceBilling.Data.Data;
using FinanceBilling.Data.Entities;
using FinanceBilling.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinanceBilling.Data.Repositories
{
    // Repository class for Invoice operations
    // Implements IInvoiceRepository interface
    public class InvoiceRepository : IInvoiceRepository
    {
        // Database context for accessing Invoice table
        private readonly ApplicationDbContext _context;

        // Constructor Dependency Injection
        public InvoiceRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        // Get All Invoices
        public async Task<List<Invoice>> GetAllAsync()
        {
            // Retrieve all invoices from database
            return await _context.Invoices.ToListAsync();
        }

        // Get Invoice By Id
        public async Task<Invoice?> GetByIdAsync(int id)
        {
            // Retrieve invoice based on InvoiceId
            return await _context.Invoices
                                 .FirstOrDefaultAsync(x =>
                                     x.InvoiceId == id);
        }

        // Add Invoice
        public async Task AddAsync(Invoice invoice)
        {
            // Add new invoice to database
            await _context.Invoices.AddAsync(invoice);

            // Save changes
            await _context.SaveChangesAsync();
        }

        // Update Invoice
        public async Task UpdateAsync(Invoice invoice)
        {
            // Find existing invoice
            var existingInvoice =
                await _context.Invoices
                              .FirstOrDefaultAsync(x =>
                                  x.InvoiceId == invoice.InvoiceId);

            // If invoice does not exist, return
            if (existingInvoice == null)
            {
                return;
            }

            // Update Invoice Number
            existingInvoice.InvoiceNumber =
                invoice.InvoiceNumber;

            // Update Customer Name
            existingInvoice.CustomerName =
                invoice.CustomerName;

            // Update Invoice Date
            existingInvoice.InvoiceDate =
                invoice.InvoiceDate;

            // Update Total Amount
            existingInvoice.TotalAmount =
                invoice.TotalAmount;

            // Update Status
            existingInvoice.Status =
                invoice.Status;

            // Save updated data
            await _context.SaveChangesAsync();
        }

        // Delete Invoice
        public async Task DeleteAsync(int id)
        {
            // Find invoice by Id
            var invoice =
                await _context.Invoices.FindAsync(id);

            // If invoice exists, delete it
            if (invoice != null)
            {
                _context.Invoices.Remove(invoice);

                // Save changes
                await _context.SaveChangesAsync();
            }
        }
    }
}