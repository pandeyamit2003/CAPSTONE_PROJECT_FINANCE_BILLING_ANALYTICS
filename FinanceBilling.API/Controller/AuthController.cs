using FinanceBilling.Data.Data;
using FinanceBilling.Data.DTOs;
using FinanceBilling.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinanceBilling.API.Services;

namespace FinanceBilling.API.Controllers
{
    // Marks this class as an API controller
    [ApiController]

    // Sets the route pattern as api/Auth
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // Database context for accessing tables
        private readonly ApplicationDbContext _context;

        // Configuration object for reading JWT settings
        private readonly IConfiguration _configuration;

        // Service used to send emails such as OTPs, password reset links, and notifications
        private readonly EmailService _emailService;

        // Constructor Dependency Injection
        public AuthController(
          ApplicationDbContext context,
          IConfiguration configuration,
          EmailService emailService)
           {
            _context = context;
            _configuration = configuration;
            _emailService = emailService;
           }

        // REGISTER

        // Endpoint: POST api/Auth/register
        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            
            // Check whether RoleId exists in Roles table
            var roleExists = _context.Roles.Any(r => r.RoleId == dto.RoleId);

            // If role does not exist, return Bad Request
            if (!roleExists)
            {
                return BadRequest("Invalid RoleId");
            }

            // Check if email already exists
            var existingEmail =
                _context.Users
                        .Any(x => x.Email.ToLower() ==
                                  dto.Email.ToLower());

            if (existingEmail)
            {
                return BadRequest("Email already exists");
            }

            // Check if username already exists
            var existingUser =
                _context.Users
                        .Any(x => x.UserName.ToLower() ==
                                  dto.UserName.ToLower());

            if (existingUser)
            {
                return BadRequest("Username already exists");
            }

            // Create a new User object from DTO data
            var user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,

                // Storing password 
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                
                // Assign selected role
                RoleId = dto.RoleId
            };

            // Add user to Users table
            _context.Users.Add(user);

            // Save changes to database
            _context.SaveChanges();

            // Return success response
            return Ok("User Registered");
        }

        // LOGIN


        // Endpoint: POST api/Auth/login
        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {

            // Find user by username
            var user =
         _context.Users
         .AsEnumerable()
         .FirstOrDefault(x =>
             x.UserName == dto.UserName);

            Console.WriteLine("Username Entered: " + dto.UserName);
            Console.WriteLine("User Found: " + (user?.UserName ?? "NULL"));


            // Check username exists
            if (user == null)
            {
                return Unauthorized("Invalid Username or Password");
            }

            // Check password 
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid Username or Password");
            }

            // Create JWT claims
            var claims = new[]
            {
                // Store username in token
                new Claim(ClaimTypes.Name,user.UserName),

                // Store role in token
                new Claim(ClaimTypes.Role,user.RoleId.ToString())
            };

            // Create security key from JWT Key in appsettings.json
            var key =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        _configuration["Jwt:Key"]));

            // Create signing credentials using HmacSha256 algorithm
            var credentials =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

            // Generate JWT Token
            var token =
                new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(2),
                    signingCredentials: credentials);

            // Return generated token and user role
            return Ok(new
            {
                token =
         new JwtSecurityTokenHandler()
             .WriteToken(token),

                role = user.RoleId
            });
        }

        /// Sends OTP to registered email
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(
            ForgotPasswordDto dto)
        {
            // Find user by email
            var user = _context.Users
                .FirstOrDefault(x => x.Email == dto.Email);

            if (user == null)
            {
                return NotFound("Email not found");
            }

            // Generate 6 digit OTP
            var otp = new Random()
                .Next(100000, 999999)
                .ToString();

            // Store OTP
            user.OTP = otp;

            // OTP valid for 10 minutes
            user.OTPExpiry = DateTime.Now.AddMinutes(10);

            await _context.SaveChangesAsync();

            return Ok("OTP sent successfully");
        }
        
        /// Verifies OTP and creates reset token
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp(
            VerifyOtpDto dto)
        {
            //Find User by Email
            var user = _context.Users
                .FirstOrDefault(x => x.Email == dto.Email);

            if (user == null)
            {
                return NotFound("User not found");
            }

            // Check OTP
            if (user.OTP != dto.OTP)
            {
                return BadRequest("Invalid OTP");
            }

            // Check expiry
            if (user.OTPExpiry < DateTime.Now)
            {
                return BadRequest("OTP expired");
            }

            // Generate secure reset token
            var token = Guid.NewGuid().ToString();

            user.ResetToken = token;

            user.ResetTokenExpiry =
                DateTime.Now.AddMinutes(15);

            // Remove OTP after successful verification
            user.OTP = null;
            user.OTPExpiry = null;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                ResetToken = token
            });
        }

        /// Resets password using token
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(
            ResetPasswordDto dto)
        {
            var user = _context.Users
                .FirstOrDefault(x =>
                    x.ResetToken == dto.Token);

            if (user == null)
            {
                return BadRequest("Invalid token");
            }

            if (user.ResetTokenExpiry < DateTime.Now)
            {
                return BadRequest("Token expired");
            }

            // Hash password using BCrypt
            user.PasswordHash =
                BCrypt.Net.BCrypt.HashPassword(
                    dto.NewPassword);

            // Clear token after use
            user.ResetToken = null;
            user.ResetTokenExpiry = null;

            await _context.SaveChangesAsync();

            return Ok("Password reset successful");
        }
    }
}