    using DevSpot.Data;
using DevSpot.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DevSpot.Repositories;
using DevSpot.Models;

public class Program
{
    public static async Task Main(string[] args)
    {
        // Create the builder for the Web application
        var builder = WebApplication.CreateBuilder(args);

        // Register DbContext to the DI container (dependency injection)
        builder.Services.AddDbContext<AppDbContext>(options =>

        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
        });
        builder.Services.AddDefaultIdentity<IdentityUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
        }) 
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();
        //////
        builder.Services.AddScoped<IRepository<JobPosting>,JobPostingRepository>();
        // Add services to the container (controllers and views)
        builder.Services.AddControllersWithViews();

        // Build the Web application
        var app = builder.Build();

        // Configure the HTTP request pipeline
        if (!app.Environment.IsDevelopment())
        {
            // Production error handling
            app.UseExceptionHandler("/Home/Error");
            // HSTS (HTTP Strict Transport Security) for production scenarios
            app.UseHsts();
        }
        using (var scope = app.Services.CreateScope()) { 
        var services =scope.ServiceProvider;
            RoleSeeder.SeedRolesAsync(services).Wait();
            UserSeeder.SeedUsersAsync(services).Wait();
        }

        // Use HTTPS redirection in production
        app.UseHttpsRedirection();

        // Use static file middleware (e.g., for serving CSS, JS, images)
        app.UseStaticFiles();

        // Configure routing and authorization
        app.UseRouting();
        app.UseAuthorization();
        app.MapRazorPages();

        // Map controllers (MVC) and default route configuration
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=JobPostings}/{action=Index}/{id?}");

        // Run the application
        app.Run();
    }
}
