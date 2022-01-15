using LawyerServices.Common.LawyerViewModels;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using LawyerServices.Services.Mapping;

namespace LawyerServices.Services.Data
{
    public class CompanyService : ICompanyService
    {
        private readonly IDeletableEntityRepository<Company> companyRepository;

        public CompanyService(IDeletableEntityRepository<Company> companyREpository)
        {
            this.companyRepository = companyREpository;
        }

        public async Task CreateMoreInformation(string languages, string education, string qualifications, string experience, string companyId)
        {
            var company = this.companyRepository.All().FirstOrDefault(c => c.Id == companyId);
            if (company is null) return;

            company.Languages = languages;
            company.Qualifications = qualifications;
            company.Education = education;
            company.Experience = experience;

            this.companyRepository.Update(company);
            await this.companyRepository.SaveChangesAsync();
        }

        public async Task<MoreInformationViewModel> GetMoreInformation(string companyId)
        {
            var model = this.companyRepository.All().Where(x=>x.Id == companyId).To<MoreInformationViewModel>().FirstOrDefault();
            if (model ==null)
            {
                //todo: change
                return null;
            }

            return model;
        }
    }
}
