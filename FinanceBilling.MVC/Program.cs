// Create WebApplication Builder
var builder = WebApplication.CreateBuilder(args);

// Add MVC Controller and View Services
builder.Services.AddControllersWithViews();

// Register HttpClient Service
builder.Services.AddHttpClient();

// Session Support
builder.Services.AddDistributedMemoryCache();

// Configure Session Settings
builder.Services.AddSession(options =>
{
    // Session Timeout Duration
    options.IdleTimeout = TimeSpan.FromMinutes(30);

    // Restrict Cookie Access To HTTP Only
    options.Cookie.HttpOnly = true;

    // Mark Cookie As Essential
    options.Cookie.IsEssential = true;
});

// Build Application
var app = builder.Build();

// Configure Error Handling For Production Environment
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Redirect HTTP Requests To HTTPS
app.UseHttpsRedirection();

// Enable Static Files (CSS, JS, Images)
app.UseStaticFiles();

// Enable Routing
app.UseRouting();

// Enable Session Middleware
app.UseSession();

// Enable Authorization Middleware
app.UseAuthorization();

// Configure Default Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

// Run Application
app.Run();