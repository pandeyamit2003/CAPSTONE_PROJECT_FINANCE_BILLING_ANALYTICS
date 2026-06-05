using FinanceBilling.Data.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceBilling.API.Controllers
{
    // Marks this class as an API Controller
    [ApiController]

    // Route: api/Dashboard
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        // Database context for accessing tables
        private readonly ApplicationDbContext _context;

        // Constructor Dependency Injection
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Authorize users having RoleId 1 (Admin), 2 (Accountant), or 3 (Viewer)
        [Authorize(Roles = "1,2,3")]

        // Endpoint: GET api/Dashboard/summary
        [HttpGet("summary")]
        public IActionResult Summary()
        {
            // Count total invoices
            var invoiceCount =
                _context.Invoices.Count();

            // Count total payments
            var paymentCount =
                _context.Payments.Count();

            // Calculate total invoice amount
            // If no invoices exist, return 0
            var totalInvoiceAmount =
                _context.Invoices.Any()
                ? _context.Invoices.Sum(x => x.TotalAmount)
                : 0;

            // Calculate total payment amount
            // If no payments exist, return 0
            var totalPaymentAmount =
                _context.Payments.Any()
                ? _context.Payments.Sum(x => x.AmountPaid)
                : 0;

            // Calculate today's revenue
            // Sum of payments made today
            var todayRevenue =
                _context.Payments
                        .Where(x => x.PaymentDate.Date == DateTime.Today)
                        .Sum(x => x.AmountPaid);

            // Calculate pending amount
            var pendingAmount =
                totalInvoiceAmount - totalPaymentAmount;

            // Initialize collection rate
            decimal collectionRate = 0;

            // Avoid division by zero
            if (totalInvoiceAmount > 0)
            {
                // Collection Rate = (Total Payments / Total Invoice Amount) * 100
                collectionRate =
                    (totalPaymentAmount * 100)
                    / totalInvoiceAmount;
            }

            // Generate invoice-wise payment details
            var invoiceWiseData =
                _context.Invoices
                .Select(invoice => new
                {
                    // Invoice ID
                    InvoiceId = invoice.InvoiceId,

                    // Invoice Number
                    InvoiceNumber =
                        invoice.InvoiceNumber,

                    // Total Invoice Amount
                    InvoiceAmount =
                        invoice.TotalAmount,

                    // Total Paid Amount for this invoice
                    PaidAmount =
                        _context.Payments
                        .Where(p =>
                            p.InvoiceId ==
                            invoice.InvoiceId)
                        .Sum(p =>
                            (decimal?)p.AmountPaid)
                        ?? 0,

                    // Pending Amount for this invoice
                    PendingAmount =
                        invoice.TotalAmount -
                        (
                            _context.Payments
                            .Where(p =>
                                p.InvoiceId ==
                                invoice.InvoiceId)
                            .Sum(p =>
                                (decimal?)p.AmountPaid)
                            ?? 0
                        )
                })
                .ToList();

            // Return dashboard summary data
            return Ok(new
            {
                // Total number of invoices
                InvoiceCount = invoiceCount,

                // Total number of payments
                PaymentCount = paymentCount,

                // Total invoice value
                TotalInvoiceAmount =
                    totalInvoiceAmount,

                // Total payment received
                TotalPaymentAmount =
                    totalPaymentAmount,

                // Remaining pending amount
                PendingAmount =
                    pendingAmount,

                // Total revenue collected
                TotalRevenue =
                    totalPaymentAmount,

                // Revenue collected today
                TodayRevenue =
                    todayRevenue,

                // Collection percentage
                CollectionRate =
                    Math.Round(collectionRate, 2),

                // Invoice-wise payment details
                InvoiceWiseData =
                    invoiceWiseData
            });

        }
    }
}