  using Microsoft.AspNetCore.Identity;

namespace DevSpot.Data
{
    public class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            if (! await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!await roleManager.RoleExistsAsync("JobSeeker"))
            {
                await roleManager.CreateAsync(new IdentityRole("JobSeeker"));
            }
            if (!await roleManager.RoleExistsAsync("Employer"))
            {
                await roleManager.CreateAsync(new IdentityRole("Employer"));
            }
        }
    }
}
