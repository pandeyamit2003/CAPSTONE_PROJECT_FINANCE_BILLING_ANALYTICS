using FinanceBilling.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FinanceBilling.MVC.Controllers
{
    // Controller responsible for handling Home page requests
    public class HomeController : Controller
    {
        // Logger used for application logging
        private readonly ILogger<HomeController> _logger;

        // Constructor Dependency Injection
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Displays the Home page
        public IActionResult Index()
        {
            return View();
        }

        // Displays the Privacy page
        public IActionResult Privacy()
        {
            return View();
        }

        // Displays the Error page
        // ResponseCache disables caching for error responses
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Pass RequestId to Error View
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}