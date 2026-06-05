using FinanceBilling.Data.Data;
using FinanceBilling.Data.DTOs;
using FinanceBilling.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FinanceBilling.API.Controllers
{
    // Marks this class as an API Controller
    [ApiController]

    // Route: api/Role
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        // Database context for accessing Role table
        private readonly ApplicationDbContext _context;

        // Constructor Dependency Injection
        public RoleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET ALL ROLES

        // Endpoint: GET api/Role
        [HttpGet]
        public IActionResult GetAll()
        {
            // Return all roles from database
            return Ok(_context.Roles.ToList());
        }

        // GET ROLE BY ID

        // Endpoint: GET api/Role/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Find role by RoleId
            var role = _context.Roles.Find(id);

            // Return 404 if role not found
            if (role == null)
                return NotFound();

            // Return role details
            return Ok(role);
        }

        // CREATE ROLE

        // Endpoint: POST api/Role
        [HttpPost]
        public IActionResult Create(RoleDto dto)
        {
            // Create new Role object from DTO
            var role = new Role
            {
                RoleName = dto.RoleName
            };

            // Add role to database
            _context.Roles.Add(role);

            // Save changes
            _context.SaveChanges();

            // Return success message
            return Ok("Role Created Successfully");
        }

        // DELETE ROLE

        // Endpoint: DELETE api/Role/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Find role by RoleId
            var role = _context.Roles.Find(id);

            // Return 404 if role not found
            if (role == null)
                return NotFound();

            // Remove role from database
            _context.Roles.Remove(role);

            // Save changes
            _context.SaveChanges();

            // Return success message
            return Ok("Role Deleted Successfully");
        }
    }
}