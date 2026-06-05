using FinanceBilling.Data.Entities;
using FinanceBilling.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceBilling.API.Controllers
{
    // Marks this class as an API Controller
    [ApiController]

    // Route: api/Invoice
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        // Service layer object for Invoice operations
        private readonly IInvoiceService _service;

        // Constructor Dependency Injection
        public InvoiceController(IInvoiceService service)
        {
            _service = service;
        }

        // GET ALL INVOICES

        // Accessible by Admin(1), Accountant(2), Viewer(3)
        [Authorize(Roles = "1,2,3")]

        // Endpoint: GET api/Invoice
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Fetch all invoices from service layer
            var invoices = await _service.GetAllAsync();

            // Return invoice list
            return Ok(invoices);
        }

        // GET INVOICE BY ID

        // Accessible by Admin(1), Accountant(2), Viewer(3)
        [Authorize(Roles = "1,2,3")]

        // Endpoint: GET api/Invoice/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // Fetch invoice by ID
            var invoice = await _service.GetByIdAsync(id);

            // If invoice not found return 404
            if (invoice == null)
                return NotFound();

            // Return invoice details
            return Ok(invoice);
        }

        // CREATE NEW INVOICE

        // Accessible only by Admin(1)
        [Authorize(Roles = "1")]

        // Endpoint: POST api/Invoice
        [HttpPost]
        public async Task<IActionResult> Create(Invoice invoice)
        {
            // Validate Invoice Number is not empty
            if (string.IsNullOrWhiteSpace(invoice.InvoiceNumber))
            {
                return BadRequest(
                    "Invoice Number is required");
            }

            // Validate Invoice Number contains only letters and numbers
            if (!System.Text.RegularExpressions.Regex
                .IsMatch(invoice.InvoiceNumber, @"^[A-Za-z0-9]+$"))
            {
                return BadRequest(
                    "Invoice Number can contain only letters and numbers");
            }

            // Add invoice using service layer
            await _service.AddAsync(invoice);

            // Return success message
            return Ok("Invoice Added Successfully");
        }

        // UPDATE EXISTING INVOICE

        // Accessible only by Admin(1)
        [Authorize(Roles = "1")]

        // Endpoint: PUT api/Invoice
        [HttpPut]
        public async Task<IActionResult> Update(Invoice invoice)
        {
            // Validate InvoiceId
            if (invoice.InvoiceId <= 0)
            {
                return BadRequest("InvoiceId is required");
            }

            // Update invoice using service layer
            await _service.UpdateAsync(invoice);

            // Return success message
            return Ok("Invoice Updated Successfully");
        }

        // DELETE INVOICE

        // Accessible only by Admin(1)
        [Authorize(Roles = "1")]

        // Endpoint: DELETE api/Invoice/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Delete invoice using InvoiceId
            await _service.DeleteAsync(id);

            // Return success message
            return Ok("Invoice Deleted Successfully");
        }
    }
}