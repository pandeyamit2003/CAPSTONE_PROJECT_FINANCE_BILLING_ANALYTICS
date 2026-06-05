using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace FinanceBilling.API.Services
{
    // Service responsible for sending emails such as OTPs
    public class EmailService
    {
        // Used to access EmailSettings from appsettings.json
        private readonly IConfiguration _configuration;

        // Constructor for injecting configuration settings
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Sends OTP to user's registered email address
        public async Task SendOtpAsync(
            string email,
            string otp)
        {
            // Create email message
            var message = new MimeMessage();

            // Sender email
            message.From.Add(
                MailboxAddress.Parse(
                    _configuration["EmailSettings:Email"]));

            // Receiver email
            message.To.Add(
                MailboxAddress.Parse(email));

            // Email subject
            message.Subject = "Password Reset OTP";

            // Email body
            message.Body = new TextPart("plain")
            {
                Text = $"Your OTP is: {otp}"
            };

            // Create SMTP client
            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            // Connect to Gmail SMTP server
            await smtp.ConnectAsync(
                "smtp.gmail.com",
                587,
                SecureSocketOptions.StartTls);

            // Authenticate sender email
            await smtp.AuthenticateAsync(
                _configuration["EmailSettings:Email"],
                _configuration["EmailSettings:Password"]);

            // Send email
            await smtp.SendAsync(message);

            // Disconnect SMTP client
            await smtp.DisconnectAsync(true);
        }
    }
}