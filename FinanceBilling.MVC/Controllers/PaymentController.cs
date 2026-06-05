using FinanceBilling.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace FinanceBilling.MVC.Controllers
{
    // Controller responsible for Payment Management
    public class PaymentController : Controller
    {
        // HttpClient used to communicate with Payment API
        private readonly HttpClient _httpClient;

        // Constructor Dependency Injection
        public PaymentController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        // GET: Payment List
        public async Task<IActionResult> Index()
        {
            // Get JWT Token from Session
            var token =
                HttpContext.Session.GetString("JWToken");

            // Redirect to Login page if token is missing
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction(
                    "Login",
                    "Auth");
            }

            // Attach JWT Token to Authorization Header
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer",
                    token);

            // Call API to retrieve all payments
            var response =
                await _httpClient.GetAsync(
                    "https://localhost:7282/api/Payment");

            // Handle API errors
            if (!response.IsSuccessStatusCode)
            {
                var error =
                    await response.Content.ReadAsStringAsync();

                return Content(
                    $"API Error: {response.StatusCode}\n{error}");
            }

            // Read JSON response
            var json =
                await response.Content.ReadAsStringAsync();

            // Convert JSON into PaymentViewModel list
            var payments =
                JsonSerializer.Deserialize<List<PaymentViewModel>>
                (
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );

            // Return data to View
            return View(payments);
        }

        // GET: Create Payment
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create Payment
        [HttpPost]
        public async Task<IActionResult> Create(
    PaymentViewModel payment)
        {
            // Validate form data
            if (!ModelState.IsValid)
            {
                return View(payment);
            }

            // Get JWT Token from Session
            var token =
                HttpContext.Session.GetString("JWToken");

            // Attach JWT Token to Authorization Header
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer",
                    token);

            // Convert object into JSON format
            var json =
                JsonSerializer.Serialize(payment);

            // Create HTTP request content
            var content =
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

            // Call API to create payment
            var response =
                await _httpClient.PostAsync(
                    "https://localhost:7282/api/Payment",
                    content);

            // Redirect to Payment List if successful
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            
            // Read API error
            var error =
                await response.Content.ReadAsStringAsync();

            ViewBag.AmountError = error;
           
            // Stay on same page and show error
            return View(payment);
        }

        // Display Payment Details
        public async Task<IActionResult> Details(int id)
        {
            // Get JWT Token
            var token =
                HttpContext.Session.GetString("JWToken");

            // Attach JWT Token to Authorization Header
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer",
                    token);

            // Call API to get payment by Id
            var response =
                await _httpClient.GetAsync(
                    $"https://localhost:7282/api/Payment/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            // Read JSON response
            var json =
                await response.Content.ReadAsStringAsync();

            // Convert JSON into PaymentViewModel
            var payment =
                JsonSerializer.Deserialize<PaymentViewModel>
                (
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );

            return View(payment);
        }

        // Display Edit Payment Page
        public async Task<IActionResult> Edit(int id)
        {
            // Get JWT Token
            var token =
                HttpContext.Session.GetString("JWToken");

            // Attach JWT Token to Authorization Header
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer",
                    token);

            // Call API to get payment by Id
            var response =
                await _httpClient.GetAsync(
                    $"https://localhost:7282/api/Payment/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            // Read JSON response
            var json =
                await response.Content.ReadAsStringAsync();

            // Convert JSON into PaymentViewModel
            var payment =
                JsonSerializer.Deserialize<PaymentViewModel>
                (
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );

            return View(payment);
        }

        // Update Existing Payment
        [HttpPost]
        public async Task<IActionResult> Edit(
    PaymentViewModel payment)
        {
            // Get JWT Token
            var token =
                HttpContext.Session.GetString("JWToken");

            // Attach JWT Token to Authorization Header
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer",
                    token);

            // Convert object into JSON format
            var json =
                JsonSerializer.Serialize(payment);

            // Create HTTP request content
            var content =
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

            // Call API to update payment
            var response =
                await _httpClient.PutAsync(
                    "https://localhost:7282/api/Payment",
                    content);

            // Redirect if successful
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            // Read API error
            var error =
                await response.Content.ReadAsStringAsync();

            return Content(error);
        }

        // Delete Payment
        public async Task<IActionResult> Delete(int id)
        {
            // Get JWT Token
            var token =
                HttpContext.Session.GetString("JWToken");

            // Attach JWT Token to Authorization Header
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer",
                    token);

            // Call API to delete payment
            var response =
                await _httpClient.DeleteAsync(
                    $"https://localhost:7282/api/Payment/{id}");

            // Redirect if successful
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            // Read API error
            var error =
                await response.Content.ReadAsStringAsync();

            return Content(error);
        }
    }
}