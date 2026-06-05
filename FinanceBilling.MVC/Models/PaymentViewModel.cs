using System.ComponentModel.DataAnnotations;

namespace FinanceBilling.MVC.Models
{
    // ViewModel used for Payment operations in MVC
    public class PaymentViewModel
    {
        // Unique identifier for the payment
        public int PaymentId { get; set; }

        // Invoice Id associated with the payment
        [Required(ErrorMessage = "Invoice Id is required")]
        [Range(1, int.MaxValue,
            ErrorMessage = "Please enter a valid Invoice Id")]
        public int InvoiceId { get; set; }

        // Amount paid against the invoice
        [Required(ErrorMessage = "Amount Paid is required")]
        [Range(0.01, 999999999,
            ErrorMessage = "Amount must be greater than 0")]
        public decimal AmountPaid { get; set; }

        // Date when the payment was made
        [Required(ErrorMessage = "Payment Date is required")]
        public DateTime PaymentDate { get; set; }

        // Payment method used (Cash, Card, UPI, etc.)
        [Required(ErrorMessage = "Payment Method is required")]
        [RegularExpression(@"^[A-Za-z\s]+$",
            ErrorMessage = "Payment Method must contain only letters")]
        public string PaymentMethod { get; set; }
    }
}