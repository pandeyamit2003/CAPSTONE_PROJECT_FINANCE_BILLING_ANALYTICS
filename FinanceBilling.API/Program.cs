using FinanceBilling.API.Services;
using FinanceBilling.Data.Data;
using FinanceBilling.Data.Interfaces;
using FinanceBilling.Data.Repositories;
using FinanceBilling.Data.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using FinanceBilling.API.Services;

// Create builder object for configuring services and middleware
var builder = WebApplication.CreateBuilder(args);

// Email service used for sending OTPs, password reset links, and notifications
builder.Services.AddScoped<EmailService>();

// Add controller support for API endpoints
builder.Services.AddControllers();

// Add support for API endpoint discovery
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger/OpenAPI documentation
builder.Services.AddSwaggerGen(c =>
{
    // Create Swagger document
    c.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "Finance Billing API",
            Version = "v1"
        });

    // JWT Authentication Button in Swagger
    c.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            // Header name
            Name = "Authorization",

            // HTTP authentication type
            Type = SecuritySchemeType.Http,

            // Bearer token scheme
            Scheme = "bearer",

            // Token format
            BearerFormat = "JWT",

            // Token passed in request header
            In = ParameterLocation.Header
        });

    // Apply JWT security globally in Swagger
    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
});



// DATABASE CONNECTION:

// Register ApplicationDbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});


// REPOSITORY REGISTRATION:

// Register Invoice Repository for Dependency Injection
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();


// SERVICE REGISTRATION:


// Register Invoice Service for Dependency Injection
builder.Services.AddScoped<IInvoiceService, InvoiceService>();


// BUILD APPLICATION:

// Register Payment Repository
builder.Services.AddScoped
<
    IPaymentRepository,
    PaymentRepository
>();

// Register Payment Service
builder.Services.AddScoped
<
    IPaymentService,
    PaymentService
>();

// Configure JWT Authentication
builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters =
        new TokenValidationParameters
        {
            // Validate token issuer
            ValidateIssuer = true,

            // Validate token audience
            ValidateAudience = true,

            // Validate token expiry time
            ValidateLifetime = true,

            // Validate signing key
            ValidateIssuerSigningKey = true,

            // Expected issuer value
            ValidIssuer =
                builder.Configuration["Jwt:Issuer"],

            // Expected audience value
            ValidAudience =
                builder.Configuration["Jwt:Audience"],

            // Secret key used to verify token signature
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        builder.Configuration["Jwt:Key"]))
        };
});

// Add Role-Based Authorization support
builder.Services.AddAuthorization();

// Build application
var app = builder.Build();




// MIDDLEWARE PIPELINE:

// Enable Swagger only in Development Environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirect HTTP requests to HTTPS
app.UseHttpsRedirection();

// Enable JWT Authentication Middleware
app.UseAuthentication();

// Enable Authorization Middleware
app.UseAuthorization();

// Map Controller Routes
app.MapControllers();

// Run the application
app.Run();