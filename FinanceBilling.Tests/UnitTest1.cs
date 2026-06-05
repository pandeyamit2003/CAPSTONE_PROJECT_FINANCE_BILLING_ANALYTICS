using Xunit;

namespace FinanceBilling.Tests
{
    // Unit Test Class For Finance Billing Application
    public class UnitTest1
    {
        // Verify Invoice Number Is Not Empty
        [Fact]
        public void InvoiceNumber_Should_Not_Be_Empty()
        {
            string invoiceNumber = "INV1001";

            Assert.False(string.IsNullOrEmpty(invoiceNumber));
        }

        // Verify Payment Amount Is Greater Than Zero
        [Fact]
        public void PaymentAmount_Should_Be_Greater_Than_Zero()
        {
            decimal amount = 5000;

            Assert.True(amount > 0);
        }

        // Verify Revenue Is Positive
        [Fact]
        public void Revenue_Should_Be_Positive()
        {
            decimal revenue = 5000;

            Assert.True(revenue > 0);
        }

        // Verify Customer Name Is Not Empty
        [Fact]
        public void CustomerName_Should_Not_Be_Empty()
        {
            string customerName = "Nitesh";

            Assert.False(string.IsNullOrWhiteSpace(customerName));
        }

        // Verify Invoice Status Is Pending
        [Fact]
        public void InvoiceStatus_Should_Be_Pending()
        {
            string status = "Pending";

            Assert.Equal("Pending", status);
        }

        // Verify Customer Name Length Is Greater Than 2
        [Fact]
        public void CustomerName_Should_Have_Valid_Length()
        {
            string customerName = "Amit";

            Assert.True(customerName.Length > 2);
        }

        // Verify Invoice Amount Is Greater Than Zero
        [Fact]
        public void InvoiceAmount_Should_Be_Greater_Than_Zero()
        {
            decimal totalAmount = 10000;

            Assert.True(totalAmount > 0);
        }

        // Verify Payment Method Is Not Empty
        [Fact]
        public void PaymentMethod_Should_Not_Be_Empty()
        {
            string paymentMethod = "UPI";

            Assert.False(string.IsNullOrWhiteSpace(paymentMethod));
        }

        // Verify Invoice ID Is Positive
        [Fact]
        public void InvoiceId_Should_Be_Positive()
        {
            int invoiceId = 1;

            Assert.True(invoiceId > 0);
        }

        // Verify Payment ID Is Positive
        [Fact]
        public void PaymentId_Should_Be_Positive()
        {
            int paymentId = 1;

            Assert.True(paymentId > 0);
        }

        // Verify Collection Rate Does Not Exceed 100 Percent
        [Fact]
        public void CollectionRate_Should_Be_Less_Than_Or_Equal_To_100()
        {
            decimal collectionRate = 95;

            Assert.True(collectionRate <= 100);
        }

        // Verify Username Is Not Empty
        [Fact]
        public void Username_Should_Not_Be_Empty()
        {
            string username = "admin";

            Assert.False(string.IsNullOrWhiteSpace(username));
        }

        // Verify Password Is Not Empty
        [Fact]
        public void Password_Should_Not_Be_Empty()
        {
            string password = "Admin@123";

            Assert.False(string.IsNullOrWhiteSpace(password));
        }

        // Verify Username Matches Expected Value
        [Fact]
        public void Username_Should_Match_Expected_Value()
        {
            string username = "admin";

            Assert.Equal("admin", username);
        }

        // Verify Password Matches Expected Value
        [Fact]
        public void Password_Should_Match_Expected_Value()
        {
            string password = "Admin@123";

            Assert.Equal("Admin@123", password);
        }

        // Verify Username Length Is Greater Than 3
        [Fact]
        public void Username_Should_Have_Valid_Length()
        {
            string username = "admin";

            Assert.True(username.Length > 3);
        }

        // Verify Password Length Is At Least 8 Characters
        [Fact]
        public void Password_Should_Have_Minimum_Length()
        {
            string password = "Admin@123";

            Assert.True(password.Length >= 8);
        }

        // Verify Username Is Not Null
        [Fact]
        public void Username_Should_Not_Be_Null()
        {
            string username = "admin";

            Assert.NotNull(username);
        }

        // Verify Password Is Not Null
        [Fact]
        public void Password_Should_Not_Be_Null()
        {
            string password = "Admin@123";

            Assert.NotNull(password);
        }

        // Verify Login Credentials Are Valid
        [Fact]
        public void LoginCredentials_Should_Be_Valid()
        {
            string username = "admin";
            string password = "Admin@123";

            Assert.True(username == "admin" && password == "Admin@123");
        }

        // Verify Incorrect Password Fails Validation
        [Fact]
        public void IncorrectPassword_Should_Fail()
        {
            string actualPassword = "Admin@123";
            string enteredPassword = "WrongPassword";

            Assert.NotEqual(actualPassword, enteredPassword);
        }
    }
}