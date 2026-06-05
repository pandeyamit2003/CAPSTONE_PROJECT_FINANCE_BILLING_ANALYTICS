using FinanceBilling.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FinanceBilling.MVC.Controllers
{
    // Controller responsible for displaying dashboard analytics
    public class DashboardController : Controller
    {
        // HttpClient used to communicate with the API
        private readonly HttpClient _httpClient;

        // Constructor Dependency Injection
        public DashboardController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        // Dashboard Home Page
        public async Task<IActionResult> Index()
        {
            // Retrieve JWT Token from Session
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

            // Call Dashboard Summary API
            var response =
                await _httpClient.GetAsync(
                    "https://localhost:7282/api/Dashboard/summary");

            // Handle API errors
            if (!response.IsSuccessStatusCode)
            {
                var error =
                    await response.Content.ReadAsStringAsync();

                return Content(
                    $"Status: {response.StatusCode}\n\n{error}");
            }

            // Read API response as JSON string
            var json =
                await response.Content.ReadAsStringAsync();

            // Convert JSON response into ViewModel object
            var dashboard =
                JsonSerializer.Deserialize<RevenueAnalyticsViewModel>
                (
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );

            // Pass dashboard data to View
            return View(dashboard);
        }
    }
}