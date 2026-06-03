using Microsoft.EntityFrameworkCore;
using PaulAlarba.Models.Data;
using PaulAlarba.Services;
using dotenv.net;

// 1. Load .env variables BEFORE the builder is created
// This ensures builder.Configuration can see the variables in your .env file
DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<ContactMessageStore>();

// Database Configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

// Ensure you are using .NET 9+ for MapStaticAssets, 
// otherwise use app.UseStaticFiles();
app.MapStaticAssets(); 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
