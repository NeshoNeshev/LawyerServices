using LawyerServices.Common.AreasOfActivityViewModels;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using LawyerServices.Services.Mapping;

namespace LawyerServices.Services.Data.AdminServices.AreasOfActivityServices
{


    public class AreasOfActivityService : IAreasOfActivityService
    {
        private readonly IDeletableEntityRepository<AreasOfActivity> areaRepository;
        private readonly IDeletableEntityRepository<AreasCompany> areaCompanyRepository;
        private readonly IDeletableEntityRepository<Company> companyRepository;
        public AreasOfActivityService(IDeletableEntityRepository<AreasOfActivity> areaRepository, IDeletableEntityRepository<Company> companyRepository, IDeletableEntityRepository<AreasCompany> areaCompanyRepository)
        {
            this.areaRepository = areaRepository;
            this.companyRepository = companyRepository;
            this.areaCompanyRepository = areaCompanyRepository;
        }

        //todo:change
        public IEnumerable<AreasOfActivityViewModel> GetAllAreasByCompanyId(string companyId)
        {
            return this.areaCompanyRepository.All().Where(c => c.CompanyId == companyId).Select(x => x.AreasOfActivity).To<AreasOfActivityViewModel>().ToList();
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
        // Test: create map
        public async Task CreateArea(string companyId, AreasOfActivityInputModel areaModel)
        {
            if (companyId == null)
            {
                return;
            }
            
            var list = new List<string>();

            foreach (var propertyInfo in areaModel.GetType().GetProperties())
            {
                var areaValue = propertyInfo.GetValue(areaModel);
                if (areaValue != null)
                {
                    list.Add(areaValue.ToString());
                }
            }

            var allAreas = this.areaRepository.All().ToList();
            var ids = new Queue<string>();

            foreach (var item in allAreas)
            {
                if (list.Contains(item.Name))
                {
                    ids.Enqueue(item.Id);
                }
            }

            var company = this.companyRepository.All().FirstOrDefault(c => c.Id == companyId);
            if (company is null) return;

            var companyAreas = this.areaCompanyRepository.All().Where(x=>x.CompanyId == companyId).Select(x=>x.AreasOfActivityId).ToList();
            while (true)
            {
                if (ids.Count == 0)
                {
                    break;
                }
                var id = ids.Dequeue();
                if (companyAreas.Contains(id))
                {
                    continue;
                }
                var areasCompany = new AreasCompany()
                {
                    AreasOfActivityId = id,
                    CompanyId = company.Id,
                };
               
                await this.areaCompanyRepository.AddAsync(areasCompany);
                
            }
            this.areaCompanyRepository.SaveChangesAsync();



            //area.AreasCompanies.Add(areasCompany);

            //this.companyRepository.Update(company);
            //this.companyRepository.SaveChangesAsync();

            //this.areaRepository.Update(area);
            this.areaRepository.SaveChangesAsync();

        }
    }
}

