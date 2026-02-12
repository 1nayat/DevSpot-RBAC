        using Microsoft.AspNetCore.Identity;
using DevSpot.Constants;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DevSpot.Data
{          
    public class UserSeeder
    {
        public static async Task SeedUsersAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            await CreateUserWithRole(userManager, "admin@devspot.com", "Admin123!", Roles.Admin);
            await CreateUserWithRole(userManager, "jobseeker@devspot.com", "JobSeeker123!", Roles.JobSeeker);
            await CreateUserWithRole(userManager, "employer@devspot.com", "Employer123!", Roles.Employer);
        }
        //Because you're using IdentityUser, you're limited to these built-in properties:
        private static async Task CreateUserWithRole(
            UserManager<IdentityUser> userManager,
            string email, string password, string role)
        {
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new IdentityUser
                {
                    Email = email,
                    EmailConfirmed = true,
                    UserName = email
                };  

                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
                else
                {
                    // Log errors and throw exception
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error creating user {email}: {error.Description}");
                    }

                    throw new Exception($"Failed to create user with email {user.Email}. Errors: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
            else
            {
                Console.WriteLine($"User with email {email} already exists.");
            }
        }
    }
}
