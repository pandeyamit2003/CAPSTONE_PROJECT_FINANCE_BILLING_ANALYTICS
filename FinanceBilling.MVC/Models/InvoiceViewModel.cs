using System.ComponentModel.DataAnnotations;

namespace FinanceBilling.MVC.Models
{
    // ViewModel used for Invoice operations in MVC
    public class InvoiceViewModel
    {
        // Unique identifier for the invoice
        public int InvoiceId { get; set; }

        // Invoice number entered by the user
        [Required(ErrorMessage = "Invoice Number is required")]
        [RegularExpression(@"^[A-Za-z0-9\-]+$",
     ErrorMessage = "Invoice Number can contain only letters, numbers and hyphen (-)")]
        public string InvoiceNumber { get; set; }

        // Customer name
        [Required(ErrorMessage = "Customer Name is required")]
        [RegularExpression(@"^[A-Za-z\s]+$",
            ErrorMessage = "Customer Name can contain only letters")]
        public string CustomerName { get; set; }

        // Total invoice amount
        [Required(ErrorMessage = "Amount is required")]
        [Range(1, 999999999,
            ErrorMessage = "Amount must be greater than 0")]
        public decimal TotalAmount { get; set; }

        // Date of invoice creation
        public DateTime InvoiceDate { get; set; }

        // Invoice status (Paid, Pending, etc.)
        [Required]
        public string Status { get; set; }
    }
}