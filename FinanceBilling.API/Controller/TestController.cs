using Microsoft.AspNetCore.Mvc;

namespace FinanceBilling.API.Controllers
{
    // Marks this class as an API Controller
    [ApiController]

    // Route: api/Test
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        // Endpoint: GET api/Test
        [HttpGet]
        public IActionResult Get()
        {
            // Returns a success message to verify API is running
            return Ok("Finance Billing API Working");
        }
    }
}