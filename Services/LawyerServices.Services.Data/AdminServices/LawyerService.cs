using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;

namespace LawyerServices.Services.Data.AdminServices
{
    public class LawyerService : ILawyerService
    {
        private IDeletableEntityRepository<Company> companyRepository;

        private IDeletableEntityRepository<ApplicationUser> userRepository;
        public LawyerService(IDeletableEntityRepository<Company> companyRepository, IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.companyRepository = companyRepository;
            this.userRepository = userRepository;
        }

        public string CrreateLawyer()
        {
            var existingUser = this.userRepository.All().Any();
            return null;
        }
    }
}
