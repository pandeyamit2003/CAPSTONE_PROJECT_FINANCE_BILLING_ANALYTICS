using FinanceBilling.Data.Data;
using FinanceBilling.Data.Entities;
using FinanceBilling.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinanceBilling.Data.Repositories
{
    // Repository class for Payment operations
    // Implements IPaymentRepository interface
    public class PaymentRepository : IPaymentRepository
    {
        // Database context for accessing Payment table
        private readonly ApplicationDbContext _context;

        // Constructor Dependency Injection
        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all payments from database
        public async Task<List<Payment>> GetAllAsync()
        {
            return await _context.Payments.ToListAsync();
        }

        // Get payment by PaymentId
        public async Task<Payment?> GetByIdAsync(int id)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(x => x.PaymentId == id);
        }

        // Add a new payment
        public async Task AddAsync(Payment payment)
        {
            // Get Invoice Details
            var invoice = await _context.Invoices
                .FirstOrDefaultAsync(i =>
                    i.InvoiceId == payment.InvoiceId);

            // Check Invoice Exists
            if (invoice == null)
            {
                throw new Exception("Invoice not found");
            }

            // Calculate Total Amount Already Paid
            var totalPaid = await _context.Payments
                .Where(p => p.InvoiceId == payment.InvoiceId)
                .SumAsync(p => p.AmountPaid);

            // Calculate Remaining Balance
            var remainingBalance =
                invoice.TotalAmount - totalPaid;

            // Prevent Overpayment
            if (payment.AmountPaid > remainingBalance)
            {
                throw new Exception(
                    $"Payment cannot exceed remaining balance ({remainingBalance})");
            }

            // Save Payment
            await _context.Payments.AddAsync(payment);

            await _context.SaveChangesAsync();

            // Recalculate Total Paid Amount
            totalPaid = await _context.Payments
                .Where(p => p.InvoiceId == payment.InvoiceId)
                .SumAsync(p => p.AmountPaid);

            // Update Invoice Status
            if (totalPaid <= 0)
            {
                invoice.Status = "Pending";
            }
            else if (totalPaid < invoice.TotalAmount)
            {
                invoice.Status = "Partial Paid";
            }
            else
            {
                invoice.Status = "Paid";
            }

            await _context.SaveChangesAsync();
        }

        // Update existing payment
        public async Task UpdateAsync(Payment payment)
        {
            // Find payment by PaymentId
            var existingPayment =
                await _context.Payments
                              .FirstOrDefaultAsync(x =>
                                  x.PaymentId == payment.PaymentId);

            // If payment does not exist, return
            if (existingPayment == null)
            {
                return;
            }

            // Update InvoiceId
            existingPayment.InvoiceId =
                payment.InvoiceId;

            // Update AmountPaid
            existingPayment.AmountPaid =
                payment.AmountPaid;

            // Update PaymentDate
            existingPayment.PaymentDate =
                payment.PaymentDate;

            // Update PaymentMethod
            existingPayment.PaymentMethod =
                payment.PaymentMethod;

            // Save updated data
            await _context.SaveChangesAsync();
        }

        // Delete payment by Id
        public async Task DeleteAsync(int id)
        {
            // Find payment by PaymentId
            var payment =
                await _context.Payments.FindAsync(id);

            // If payment exists, remove it
            if (payment != null)
            {
                _context.Payments.Remove(payment);

                // Save changes
                await _context.SaveChangesAsync();
            }
        }
    }
}