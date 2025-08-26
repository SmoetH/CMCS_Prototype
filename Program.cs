using Microsoft.EntityFrameworkCore;
using CMCS_Prototype.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// This line configures the database to use a SQLite file.
builder.Services.AddDbContext<CmcContext>(options =>
    options.UseSqlite("Data Source=CMCS.db"));

// Register the session service with the dependency injection container.
builder.Services.AddSession();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Enable the session middleware.
app.UseRouting();

app.UseAuthorization();

// This line must come before app.UseEndpoints() and app.MapControllerRoute().
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
