using LawyerServices.Common;
using LawyerServices.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace LawyerServices.Data.Seeding
{
    internal class UserSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            await SeedUserAsync(userManager, GlobalConstants.AdministratorUserName, GlobalConstants.AdministratorPassword, GlobalConstants.AdministratorEmail);

            await SeedUserToRole(userManager, GlobalConstants.AdministratorUserName, GlobalConstants.AdministratorRoleName);
        }

        private static async Task SeedUserToRole(UserManager<ApplicationUser> userManager, string username, string roleName)
        {
            var user = await userManager.FindByNameAsync(username);

            await userManager.AddToRoleAsync(user, roleName);
        }

        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, string username, string password, string email)
        {
            var user = await userManager.FindByNameAsync(username);

            if (user == null)
            {
                var result = await userManager.CreateAsync(
                    new ApplicationUser
                    {
                        //FirstName = "Nesho",
                        //LastName ="Neshev",
                        UserName = username,
                        Email = email,
                        EmailConfirmed = true,

                    }, GlobalConstants.AdministratorPassword);

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
