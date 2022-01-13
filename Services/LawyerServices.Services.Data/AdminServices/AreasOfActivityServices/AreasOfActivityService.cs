using LawyerServices.Common.LawyerViewModels;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using LawyerServices.Services.Mapping;

namespace LawyerServices.Services.Data.AdminServices.AreasOfActivityServices
{    
    
   
    public class AreasOfActivityService : IAreasOfActivityService
    {
        private readonly IDeletableEntityRepository<AreasOfActivity> areaRepository;
        private readonly IDeletableEntityRepository<Company> companyRepository;
        public AreasOfActivityService(IDeletableEntityRepository<AreasOfActivity> areaRepository, IDeletableEntityRepository<Company> companyRepository)
        {
            this.areaRepository = areaRepository;
            this.companyRepository = companyRepository;
        }
       
        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<AreasOfActivity> query = this.areaRepository.All().OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
        // Test: Not work corectly
        public async Task CreateArea(string userId, string companyId, string copyright)
        {


            var area = this.areaRepository.All().FirstOrDefault(x => x.Name == copyright);
            if (area is null) return;

            var company = this.companyRepository.All().FirstOrDefault(c => c.Id == companyId);
            var r = company.AreasCompanies.Count;
            if (company is null) return;

            var areasCompany = new AreasCompany()
            {
                AreasOfActivityId = area.Id,
                CompanyId = company.Id,
            };
            company.AreasCompanies.Add(areasCompany);
            area.AreasCompanies.Add(areasCompany);

            this.companyRepository.Update(company);
            await this.companyRepository.SaveChangesAsync();

            this.areaRepository.Update(area);
            await this.areaRepository.SaveChangesAsync();

        }
    }
}

