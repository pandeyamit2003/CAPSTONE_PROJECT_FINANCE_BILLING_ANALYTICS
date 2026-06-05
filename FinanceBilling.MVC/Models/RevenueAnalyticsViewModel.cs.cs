namespace FinanceBilling.MVC.Models
{
    // ViewModel used for Dashboard Revenue Analytics
    public class RevenueAnalyticsViewModel
    {
        // Total number of invoices
        public int InvoiceCount { get; set; }

        // Total number of payments
        public int PaymentCount { get; set; }

        // Total revenue collected
        public decimal TotalRevenue { get; set; }

        // Revenue collected today
        public decimal TodayRevenue { get; set; }

        // Percentage of invoice amount collected
        public decimal CollectionRate { get; set; }

        // Sum of all invoice amounts
        public decimal TotalInvoiceAmount { get; set; }

        // Sum of all payment amounts
        public decimal TotalPaymentAmount { get; set; }

        // Remaining unpaid amount
        public decimal PendingAmount { get; set; }

        // Invoice-wise revenue and payment details
        public List<InvoiceWiseData> InvoiceWiseData { get; set; }
    = new();
    }
}