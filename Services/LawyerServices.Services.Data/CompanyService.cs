using LawyerServices.Common.CompanyViewModels;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using System.Linq;

namespace LawyerServices.Services.Data
{
    public class CompanyService : ICompanyService
    {
        private readonly IDeletableEntityRepository<Company> companyRepository;

        public CompanyService(IDeletableEntityRepository<Company> companyREpository)
        {
            this.companyRepository = companyREpository;
        }
    }
}
