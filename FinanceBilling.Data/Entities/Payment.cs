namespace FinanceBilling.Data.Entities
{
    // Entity class representing the Payments table
    public class Payment
    {
        // Primary Key of the Payment table
        public int PaymentId { get; set; }

        // Foreign Key referencing the Invoice table
        public int InvoiceId { get; set; }

        // Amount paid against the invoice
        public decimal AmountPaid { get; set; }

        // Date when the payment was made
        public DateTime PaymentDate { get; set; }

        // Payment method used (Cash, Card, UPI, etc.)
        public string PaymentMethod { get; set; }

        
    }
}