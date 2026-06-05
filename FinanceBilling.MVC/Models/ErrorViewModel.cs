namespace FinanceBilling.MVC.Models
{
    // ViewModel used for displaying error information
    public class ErrorViewModel
    {
        // Stores the unique request identifier
        public string? RequestId { get; set; }

        // Returns true if RequestId is available
        // Used to determine whether RequestId should be displayed
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}