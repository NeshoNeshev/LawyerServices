using LawyerServices.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace LawyerServices.Data.Seeding
{
    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new List<string> { "Moderator", "Company", GlobalConstants.AdministratorRoleName, "Lawyer", "Notary" };
            await SeedRoleAsync(roleManager, roles);
        }

        private static async Task SeedRoleAsync(RoleManager<IdentityRole> roleManager, List<string> roles)
        {
            foreach (var item in roles)
            {
                var role = await roleManager.FindByNameAsync(item);
                if (role == null)
                {
                    var result = await roleManager.CreateAsync(new IdentityRole(item));
                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                    }
                }
            }

        }
    }
}
