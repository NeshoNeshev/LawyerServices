
using LawyerServices.Services.Mapping;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using LaweyrServices.Web.Shared.ProfileModels;

namespace LawyerServices.Services.Data
{
    public class CurrentProfileService : ICurrentProfileService
    {
        private readonly IDeletableEntityRepository<Company> companyRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        public CurrentProfileService(IDeletableEntityRepository<Company> companyrepository, IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.companyRepository = companyrepository;
            this.userRepository = userRepository;
        }


        public LawyerProfileViewModel GetLawyerProfileInformation(string Id)
        {
            var lawyer = this.companyRepository.All().Where(x => x.Id == Id).To<LawyerProfileViewModel>().FirstOrDefault();

            return lawyer;
        }

        public async Task EditLawyerProfileInformationAsync(EditLawyerProfileModel model)
        {
            var lawyer = this.companyRepository.All().FirstOrDefault(x => x.Id == model.Id);
            var user = this.userRepository.All().FirstOrDefault(x=>x.Id == model.userId);
            if (lawyer == null) return;
            try
            {
                lawyer.Names = model.Names;
                lawyer.Address = model.Address;
                lawyer.WebSite = model.WebSite;
                lawyer.PhoneNumbers = model.PhoneNumber;
                lawyer.IsPublicPhoneNuber = model.IsPublicPhoneNuber;
                user.PhoneNumber = model.PhoneNumber;
                this.companyRepository.Update(lawyer);
                
                this.userRepository.Update(user);
                await this.companyRepository.SaveChangesAsync();
                await this.userRepository.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new InvalidOperationException("Lawyer not created");
            }
            
        }
    }
}
