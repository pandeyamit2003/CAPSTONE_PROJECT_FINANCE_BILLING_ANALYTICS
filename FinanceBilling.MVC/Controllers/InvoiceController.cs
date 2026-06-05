using FinanceBilling.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace FinanceBilling.MVC.Controllers
{
    // Controller responsible for Invoice Management
    public class InvoiceController : Controller
    {
        // HttpClient used to communicate with Invoice API
        private readonly HttpClient _httpClient;

        // Constructor Dependency Injection
        public InvoiceController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        // Display Create Invoice Page
        public IActionResult Create()
        {
            // Get user role from Session
            var role =
                HttpContext.Session.GetString("Role");

            // Only Admin (Role = 1) can access Create page
            if (role != "1")
            {
                return RedirectToAction(
                    "Index",
                    "Home");
            }

            return View();
        }

        // Create New Invoice
        [HttpPost]
        public async Task<IActionResult> Create(
    InvoiceViewModel invoice)
        {
            // Validate form data
            if (!ModelState.IsValid)
            {
                return View(invoice);
            }

            // Get JWT Token from Session
            var token =
                HttpContext.Session.GetString("JWToken");

            // Attach token to Authorization Header
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer",
                    token);

            // Convert Invoice object into JSON
            var json =
                JsonSerializer.Serialize(invoice);

            // Create HTTP request content
            var content =
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

            // Call API to create invoice
            var response =
                await _httpClient.PostAsync(
                    "https://localhost:7282/api/Invoice",
                    content);

            // Redirect to Invoice List if successful
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            // Read API error message
            var error =
                await response.Content.ReadAsStringAsync();

            return Content(error);
        }

        // Display All Invoices
        public async Task<IActionResult> Index()
        {
            // Get JWT Token
            var token =
                HttpContext.Session.GetString("JWToken");

            // Redirect to Login if token not found
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction(
                    "Login",
                    "Auth");
            }

            // Attach token to Authorization Header
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer",
                    token);

            // Call API to get invoices
            var response =
                await _httpClient.GetAsync(
                    "https://localhost:7282/api/Invoice");

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

            // Convert JSON into InvoiceViewModel list
            var invoices =
                JsonSerializer.Deserialize<List<InvoiceViewModel>>
                (
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );

            // Send data to View
            return View(invoices);
        }

        // Display Invoice Details
        public async Task<IActionResult> Details(int id)
        {
            // Get JWT Token
            var token =
                HttpContext.Session.GetString("JWToken");

            // Attach token to Authorization Header
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer",
                    token);

            // Call API to get invoice by Id
            var response =
                await _httpClient.GetAsync(
                    $"https://localhost:7282/api/Invoice/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return Content(
                    $"API Error: {response.StatusCode}");
            }

            // Read JSON response
            var json =
                await response.Content.ReadAsStringAsync();

            // Convert JSON into InvoiceViewModel
            var invoice =
                JsonSerializer.Deserialize<InvoiceViewModel>
                (
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );

            return View(invoice);
        }

        // Display Edit Invoice Page
        public async Task<IActionResult> Edit(int id)
        {
            // Get JWT Token
            var token =
                HttpContext.Session.GetString("JWToken");

            // Attach token to Authorization Header
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer",
                    token);

            // Call API to get invoice by Id
            var response =
                await _httpClient.GetAsync(
                    $"https://localhost:7282/api/Invoice/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            // Read JSON response
            var json =
                await response.Content.ReadAsStringAsync();

            // Convert JSON into InvoiceViewModel
            var invoice =
                JsonSerializer.Deserialize<InvoiceViewModel>
                (
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );

            return View(invoice);
        }

        // Update Existing Invoice
        [HttpPost]
        public async Task<IActionResult> Edit(
    InvoiceViewModel invoice)
        {
            // Get JWT Token
            var token =
                HttpContext.Session.GetString("JWToken");

            // Attach token to Authorization Header
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer",
                    token);

            // Convert object into JSON
            var json =
                JsonSerializer.Serialize(invoice);

            // Create HTTP request content
            var content =
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

            // Call API to update invoice
            var response =
                await _httpClient.PutAsync(
                    "https://localhost:7282/api/Invoice",
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

        // Delete Invoice
        public async Task<IActionResult> Delete(int id)
        {
            // Get JWT Token
            var token =
                HttpContext.Session.GetString("JWToken");

            // Attach token to Authorization Header
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer",
                    token);

            // Call API to delete invoice
            var response =
                await _httpClient.DeleteAsync(
                    $"https://localhost:7282/api/Invoice/{id}");

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