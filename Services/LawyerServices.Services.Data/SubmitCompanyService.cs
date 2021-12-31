using LawyerServices.Common;
using LawyerServices.Data.Models;
using LawyerServices.Data.Models.Enumerations;
using LawyerServices.Data.Repositories;
using Microsoft.AspNetCore.Identity;

namespace LawyerServices.Services.Data
{
    public class SubmitCompanyService : ISubmitCompanyService
    {
        private readonly IDeletableEntityRepository<Company> companyRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public SubmitCompanyService(IDeletableEntityRepository<Company> companyRepository, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.companyRepository = companyRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task CreateAsync(SubmitApplicationModel model)
        {
            var existingUser = this.userManager.Users.Any(ph => ph.PhoneNumber == model.PhoneNumber);
            if (existingUser)
            {
                return;
            }

            var newUser = new ApplicationUser()
            {
                UserName = model.Email,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                EmailConfirmed = true,
            };
            var pass = Guid.NewGuid().ToString();
            _ = userManager.CreateAsync(newUser, pass);

            _ = userManager.AddToRoleAsync(newUser, "Lawyer");
            var profesion = Enum.Parse<Profession>(model.Profesion);
            var company = new Company() { Profession = profesion };
            company.Users.Add(newUser);

            await this.companyRepository.AddAsync(company);
            await companyRepository.SaveChangesAsync();

        }
    }
}
