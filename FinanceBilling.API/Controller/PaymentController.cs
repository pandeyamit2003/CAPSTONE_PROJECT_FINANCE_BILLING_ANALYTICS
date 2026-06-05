using FinanceBilling.Data.Entities;
using FinanceBilling.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace FinanceBilling.API.Controllers
{
    // Marks this class as an API Controller
    [ApiController]

    // Route: api/Payment
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        // Service layer dependency for Payment operations
        private readonly IPaymentService _service;

        // Constructor Dependency Injection
        public PaymentController(IPaymentService service)
        {
            _service = service;
        }

        // VIEW ALL PAYMENTS

        // Accessible by Admin(1), Accountant(2), Viewer(3)
        [Authorize(Roles = "1,2,3")]

        // Endpoint: GET api/Payment
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Return all payments
            return Ok(await _service.GetAllAsync());
        }

        // VIEW PAYMENT BY ID

        // Accessible by Admin(1), Accountant(2), Viewer(3)
        [Authorize(Roles = "1,2,3")]

        // Endpoint: GET api/Payment/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // Get payment by PaymentId
            var payment = await _service.GetByIdAsync(id);

            // Return 404 if payment not found
            if (payment == null)
                return NotFound();

            // Return payment details
            return Ok(payment);
        }

        // CREATE PAYMENT

        // Accessible by Admin(1) and Accountant(2)
        [Authorize(Roles = "1,2")]

        // Endpoint: POST api/Payment
        [HttpPost]
        public async Task<IActionResult> Create(Payment payment)
        {
            // Validate InvoiceId
            if (payment.InvoiceId <= 0)
            {
                return BadRequest("Invoice Id is required");
            }

            // Validate AmountPaid
            if (payment.AmountPaid <= 0)
            {
                return BadRequest(
                    "Amount Paid must be greater than 0");
            }

            // Validate PaymentMethod is not empty
            if (string.IsNullOrWhiteSpace(payment.PaymentMethod))
            {
                return BadRequest(
                    "Payment Method is required");
            }

            // Validate PaymentMethod contains only letters and spaces
            if (!System.Text.RegularExpressions.Regex
                .IsMatch(payment.PaymentMethod, @"^[A-Za-z\s]+$"))
            {
                return BadRequest(
                    "Payment Method must contain only letters");
            }

            try
            {
                // Add payment using service layer
                await _service.AddAsync(payment);

                //Return success message
                return Ok("Payment Added Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

            // UPDATE PAYMENT

            // Accessible by Admin(1) and Accountant(2)
            [Authorize(Roles = "1,2")]

            // Endpoint: PUT api/Payment
            [HttpPut]
            public async Task<IActionResult> Update(Payment payment)
            {
                // Validate AmountPaid
                if (payment.AmountPaid <= 0)
                {
                    return BadRequest(
                        "Amount Paid must be greater than 0");
                }

                // Validate PaymentMethod contains only letters and spaces
                if (!System.Text.RegularExpressions.Regex
                    .IsMatch(payment.PaymentMethod, @"^[A-Za-z\s]+$"))
                {
                    return BadRequest(
                        "Payment Method must contain only letters");
                }

                // Update payment
                await _service.UpdateAsync(payment);

                // Return success message
                return Ok("Payment Updated Successfully");
            }


            // DELETE PAYMENT

            // Accessible only by Admin(1)
            [Authorize(Roles = "1")]

            // Endpoint: DELETE api/Payment/{id}
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                // Delete payment using PaymentId
                await _service.DeleteAsync(id);

                // Return success message
                return Ok("Payment Deleted Successfully");
            }
        }
    }

