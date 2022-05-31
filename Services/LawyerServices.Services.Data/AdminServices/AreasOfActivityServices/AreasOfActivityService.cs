using LaweyrServices.Web.Shared.AreasOfActivityViewModels;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using LawyerServices.Services.Mapping;
using Microsoft.EntityFrameworkCore;

namespace LawyerServices.Services.Data.AdminServices.AreasOfActivityServices
{


    public class AreasOfActivityService : IAreasOfActivityService
    {
        private readonly IDeletableEntityRepository<AreasOfActivity> areaRepository;
        private readonly IDeletableEntityRepository<AreasCompany> areaCompanyRepository;
        private readonly IDeletableEntityRepository<Company> companyRepository;

        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;


        public AreasOfActivityService(IDeletableEntityRepository<AreasOfActivity> areaRepository, IDeletableEntityRepository<Company> companyRepository, IDeletableEntityRepository<AreasCompany> areaCompanyRepository, IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.areaRepository = areaRepository;
            this.companyRepository = companyRepository;
            this.areaCompanyRepository = areaCompanyRepository;
            this.userRepository = userRepository;
        }

        //todo:change
        public async Task<IEnumerable<AreasOfActivityViewModel>> GetAllAreasByCompanyId(string lawyerId)
        {
           
           
            var userAreasIds = await this.areaCompanyRepository.All().Where(c => c.CompanyId == lawyerId).Select(x => x.AreasOfActivity).To<AreasOfActivityViewModel>().ToListAsync();

            return userAreasIds;
        }
        public async Task<IEnumerable<T>> GetAll<T>(int? count = null)
        {
            IQueryable<AreasOfActivity> query = this.areaRepository.All().OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }
            var result = await query.To<T>().ToListAsync();
            return result;
        }
       
        
        public async Task CreateAreasAsync(string areaName,string lawyerId)
        {
            var area = await this.areaRepository.All().Where(x => x.Name == areaName).FirstOrDefaultAsync();
            var companyAreas = await this.areaCompanyRepository.All().Where(x => x.CompanyId == lawyerId).Select(x => x.AreasOfActivity.Name).ToListAsync();
            if (companyAreas.Contains(areaName))
            {
                return;
            }
            var areasCompany = new AreasCompany()
            {
                AreasOfActivityId = area.Id,
                CompanyId = lawyerId,
            };

            await this.areaCompanyRepository.AddAsync(areasCompany);
            await this.areaCompanyRepository.SaveChangesAsync();
        }
        public async Task DeleteAreaAsync(string areaId)
        {
            var areaTodelete = await this.areaCompanyRepository.All().FirstOrDefaultAsync(x => x.AreasOfActivityId == areaId);
            if (areaTodelete != null)
            {
                this.areaCompanyRepository.HardDelete(areaTodelete);
            }
            await this.areaCompanyRepository.SaveChangesAsync();
        }
  
    }
}

