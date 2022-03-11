
using LaweyrServices.Web.Shared;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using Microsoft.AspNetCore.Identity;

namespace LawyerServices.Services.Data
{
    public class SubmitCompanyService : ISubmitCompanyService
    {
        private readonly IDeletableEntityRepository<Request> requestRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public SubmitCompanyService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IDeletableEntityRepository<Request> requestRepository)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.requestRepository = requestRepository;
        }
        public async Task<bool> CreateRequestAsync(SubmitApplicationModel model)
        {

            //todo: move to admin
            var existingUser = this.userManager.Users.Any(ph => ph.PhoneNumber == model.PhoneNumber);
            if (existingUser)
            {

                return false;
            }

            var request = new Request()
            {
                Id = Guid.NewGuid().ToString(),
                Address = model.Address,
                Town = model.Town,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Office = model.Office,
                Profession = model.Profession,
                Names = model.Names,
            };

            await this.requestRepository.AddAsync(request);
            this.requestRepository.SaveChangesAsync();
            return true;
        }
    }
}
