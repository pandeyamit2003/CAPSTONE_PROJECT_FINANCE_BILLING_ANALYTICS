using System.Text;
using System.Text.Json;
using FinanceBilling.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinanceBilling.MVC.Controllers
{
    // Controller responsible for user authentication,
    // forgot password, OTP verification, reset password and logout
    public class AuthController : Controller
    {
        // Factory used to create HttpClient instances
        private readonly IHttpClientFactory _factory;

        // Constructor Dependency Injection
        public AuthController(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        // Display Login Page
        public IActionResult Login()
        {
            return View();
        }

        // Handle Login Form Submission
        [HttpPost]
        public async Task<IActionResult> Login(
            LoginViewModel model)
        {
            // Check if form validation passes
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Create HttpClient instance
            var client = _factory.CreateClient();

            // Convert login data into JSON format
            var json =
                JsonSerializer.Serialize(new
                {
                    userName = model.UserName,
                    password = model.Password
                });

            // Create HTTP request content
            var content =
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

            // Send login request to API
            var response =
                await client.PostAsync(
                    "https://localhost:7282/api/Auth/login",
                    content);

            // Check if login failed
            if (!response.IsSuccessStatusCode)
            {
                // Show error below Password textbox
                ModelState.AddModelError(
                    "Password",
                    "Invalid Username or Password");

                return View(model);
            }

            // Read API response body
            var responseBody =
                await response.Content.ReadAsStringAsync();

            // Parse JSON response
            using var doc =
                JsonDocument.Parse(responseBody);

            // Extract JWT Token
            var token =
                doc.RootElement
                   .GetProperty("token")
                   .GetString();

            // Extract User Role
            var role =
                doc.RootElement
                   .GetProperty("role")
                   .GetInt32()
                   .ToString();

            // Store JWT Token in Session
            HttpContext.Session.SetString(
                "JWToken",
                token ?? "");

            // Store Role in Session
            HttpContext.Session.SetString(
                "Role",
                role);

            // Redirect to Home Page after successful login
            return RedirectToAction(
                "Index",
                "Home");
        }

        // Display Forgot Password Page
        public IActionResult ForgotPassword()
        {
            return View();
        }

        // Handle Forgot Password Form Submission
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(
            ForgotPasswordViewModel model)
        {
            try
            {
                // Validate input model
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                // Create HttpClient instance
                var client = _factory.CreateClient();

                // Convert email into JSON format
                var json =
                    JsonSerializer.Serialize(new
                    {
                        email = model.Email
                    });

                // Create request content
                var content =
                    new StringContent(
                        json,
                        Encoding.UTF8,
                        "application/json");

                // Call API to send OTP
                var response =
                    await client.PostAsync(
                        "https://localhost:7282/api/Auth/forgot-password",
                        content);

                // Read response for debugging
                var responseBody =
                    await response.Content.ReadAsStringAsync();

                // Check if API request failed
                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = responseBody;
                    return View(model);
                }

                // Store email temporarily for OTP verification
                TempData["Email"] = model.Email;

                // Redirect user to OTP verification page
                return RedirectToAction("VerifyOtp");
            }
            catch (Exception ex)
            {
                // Display exception message
                ViewBag.Error = ex.Message;
                return View(model);
            }
        }

        // Display OTP Verification Page
        public IActionResult VerifyOtp()
        {
            // Create model instance
            var model =
                new VerifyOtpViewModel();

            // Retrieve email from TempData
            model.Email =
                TempData["Email"]?.ToString();

            // Keep TempData available for next request
            TempData.Keep("Email");

            return View(model);
        }

        // Handle OTP Verification
        [HttpPost]
        public async Task<IActionResult> VerifyOtp(
            VerifyOtpViewModel model)
        {
            // Validate model
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Create HttpClient instance
            var client = _factory.CreateClient();

            // Convert OTP data into JSON format
            var json =
                JsonSerializer.Serialize(new
                {
                    email = model.Email,
                    otp = model.OTP
                });

            // Create request content
            var content =
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

            // Call API to verify OTP
            var response =
                await client.PostAsync(
                    "https://localhost:7282/api/Auth/verify-otp",
                    content);

            // Check if OTP verification failed
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(
                    "",
                    "Invalid OTP");

                return View(model);
            }

            // Read API response
            var responseBody =
                await response.Content
                    .ReadAsStringAsync();

            // Parse JSON response
            using var doc =
                JsonDocument.Parse(responseBody);

            // Extract reset token
            var token =
                doc.RootElement
                   .GetProperty("resetToken")
                   .GetString();

            // Store reset token temporarily
            TempData["ResetToken"] = token;

            // Redirect to password reset page
            return RedirectToAction(
                "ResetPassword");
        }

        // Display Reset Password Page
        public IActionResult ResetPassword()
        {
            // Create model instance
            var model =
                new ResetPasswordViewModel();

            // Retrieve reset token
            model.Token =
                TempData["ResetToken"]?.ToString();

            // Keep token for next request
            TempData.Keep("ResetToken");

            return View(model);
        }

        // Handle Password Reset
        [HttpPost]
        public async Task<IActionResult> ResetPassword(
            ResetPasswordViewModel model)
        {
            // Validate model
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Create HttpClient instance
            var client = _factory.CreateClient();

            // Convert reset password data into JSON format
            var json =
                JsonSerializer.Serialize(new
                {
                    token = model.Token,
                    newPassword = model.NewPassword
                });

            // Create request content
            var content =
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

            // Call API to reset password
            var response =
                await client.PostAsync(
                    "https://localhost:7282/api/Auth/reset-password",
                    content);

            // Check if password reset failed
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(
                    "",
                    "Password reset failed");

                return View(model);
            }

            // Redirect user back to Login page
            return RedirectToAction("Login");
        }

        // Logout User
        public IActionResult Logout()
        {
            // Clear all session values
            HttpContext.Session.Clear();

            // Redirect to Login page
            return RedirectToAction("Login");
        }
    }
}