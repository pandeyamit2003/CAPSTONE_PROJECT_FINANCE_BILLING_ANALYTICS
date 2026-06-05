namespace FinanceBilling.Data.Entities
{
    // Entity class representing the Invoices table
    public class Invoice
    {
        // Primary Key of the Invoice table
        public int InvoiceId { get; set; }

        // Unique invoice number
        public string InvoiceNumber { get; set; }

        // Name of the customer
        public string CustomerName { get; set; }

        // Date when the invoice was created
        public DateTime InvoiceDate { get; set; }

        // Total invoice amount
        public decimal TotalAmount { get; set; }

        // Current status of the invoice (Paid, Pending, etc.)
        public string Status { get; set; }

       
    }
}