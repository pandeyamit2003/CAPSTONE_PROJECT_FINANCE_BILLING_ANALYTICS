namespace FinanceBilling.MVC.Models
{
    // ViewModel used for Invoice-wise payment analytics
    public class InvoiceWiseData
    {
        // Unique identifier of the invoice
        public int InvoiceId { get; set; }

        // Invoice number
        public string InvoiceNumber { get; set; }

        // Total amount of the invoice
        public decimal InvoiceAmount { get; set; }

        // Amount already paid against the invoice
        public decimal PaidAmount { get; set; }

        // Remaining amount to be paid
        public decimal PendingAmount { get; set; }
    }
}