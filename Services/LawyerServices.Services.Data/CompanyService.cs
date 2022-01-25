using LawyerServices.Common.DateModels;
using LawyerServices.Common.LawyerViewModels;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using LawyerServices.Services.Mapping;
using Microsoft.AspNetCore.Identity;

namespace LawyerServices.Services.Data
{
    public class CompanyService : ICompanyService
    {
        private readonly IDeletableEntityRepository<Company> companyRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public CompanyService(IDeletableEntityRepository<Company> companyREpository, UserManager<ApplicationUser> userManager)
        {
            this.companyRepository = companyREpository;
            this.userManager = userManager;
        }

        public async Task<string> CreateMoreInformation(string languages, string education, string qualifications, string experience, string website, string companyId, string path)
        {
            var company = this.companyRepository.All().FirstOrDefault(c => c.Id == companyId);
            if (company is null) return null;

            company.Languages = languages;
            company.Education = education;
            company.Qualifications = qualifications;
            company.Experience = experience;
            company.WebSite = website;
            company.ImgUrl = path;

            this.companyRepository.Update(company);
            this.companyRepository.SaveChangesAsync();

            return company.Id;
        }

        public MoreInformationViewModel GetMoreInformation(string companyId)
        {
            var model = this.companyRepository.All().Where(x => x.Id == companyId).To<MoreInformationViewModel>().FirstOrDefault();
            if (model == null)
            {
                //todo: change
                return null;
            }

            return model;
        }

        public async Task ChangeName(string companyId, string Name)
        {
            var company = this.companyRepository.All().FirstOrDefault(c => c.Id == companyId);
            if (company is null) return;
        }
        public async Task SaveAppointments(List<Appointment> appointments)
        {
           
            
            var company = this.companyRepository.All().FirstOrDefault(c => c.Id == "dasasasd");
            if (company is null) return;
        }
    }
}
