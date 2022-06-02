using LaweyrServices.Web.Shared.LawyerViewModels;
using LaweyrServices.Web.Shared.NotaryModels;
using LawyerServices.Data.Models;
using LawyerServices.Data.Models.Enumerations;
using LawyerServices.Data.Repositories;
using LawyerServices.Services.Mapping;
using Microsoft.EntityFrameworkCore;

namespace LawyerServices.Services.Data
{
    public class SearchService : ISearchService
    {
        private readonly IDeletableEntityRepository<AreasOfActivity> areaRepository;

        private readonly IDeletableEntityRepository<Company> companyRepository;
        private readonly IDeletableEntityRepository<Town> townRepository;
        private readonly IDeletableEntityRepository<AreasCompany> areaCompanyrepository;

        public SearchService(IDeletableEntityRepository<AreasOfActivity> areaRepository, IDeletableEntityRepository<Company> companyRepository, IDeletableEntityRepository<AreasCompany> areaCompanyrepository, IDeletableEntityRepository<Town> townRepository)
        {
            this.areaRepository = areaRepository;
            this.companyRepository = companyRepository;
            this.areaCompanyrepository = areaCompanyrepository;
            this.townRepository = townRepository;
        }

        public IEnumerable<T> SearchAllLawyersByTown<T>(string townId)
        {
            IQueryable<Company> query = this.companyRepository.All().Where(x => x.Profession == (Profession)Enum.Parse(typeof(Profession), "Lawyer")).Where(l => l.TownId == townId);

            return query.To<T>().ToList();
        }
        public IEnumerable<LawyerListItem> SearchAllLawyersByArea(string areaId)
        {

            return this.areaCompanyrepository.All().Where(x => x.AreasOfActivity.Id == areaId).Select(x => x.Company).Where(x => x.Profession == (Profession)Enum.Parse(typeof(Profession), "Lawyer")).To<LawyerListItem>();

        }
        public async Task<IEnumerable<AllLawyersModel>> SearchAsync(string? name, string? townName, string? areaName)
        {
            IEnumerable<AllLawyersModel> result;
            if (townName != null && areaName != null)
            {
                result = await this.areaCompanyrepository.All().Where(x => x.AreasOfActivity.Name.ToLower() == areaName.ToLower()).Select(x => x.Company).Where(x => x.StopAccount == false).Where(x => x.Town.Name == townName).Where(x => x.Profession == (Profession)Enum.Parse(typeof(Profession), "Lawyer")).To<AllLawyersModel>().ToListAsync();
                return result;
            }
            if (townName != null)
            {
                var lawyersInTown = await this.townRepository.All().Where(x => x.Name.ToLower() == townName.ToLower()).SelectMany(x => x.Companies).Where(x => x.Profession == (Profession)Enum.Parse(typeof(Profession), "Lawyer")).ToListAsync();
                
                if (lawyersInTown.Any())
                {
                    result = await this.companyRepository.All().Where(x => x.StopAccount == false).Where(x => x.Town.Name.ToLower() == townName.ToLower()).Where(x => x.Profession == (Profession)Enum.Parse(typeof(Profession), "Lawyer")).To<AllLawyersModel>().ToListAsync();
                    return result;
                }
                else
                {
                    result = await this.areaCompanyrepository.All().Where(x => x.AreasOfActivity.Name.ToLower() == townName.ToLower()).Select(x => x.Company).Where(x => x.StopAccount == false).Where(x => x.Profession == (Profession)Enum.Parse(typeof(Profession), "Lawyer")).To<AllLawyersModel>().ToListAsync();
                    return result;
                }
            }
            if (areaName != null)
            {
                result = await this.areaCompanyrepository.All().Where(x => x.AreasOfActivity.Name.ToLower() == areaName.ToLower()).Select(x => x.Company).Where(x => x.StopAccount == false).Where(x => x.Profession == (Profession)Enum.Parse(typeof(Profession), "Lawyer")).To<AllLawyersModel>().ToListAsync();
                return result;
            }
            
            result = await this.companyRepository.All().Where(x => x.StopAccount == false).Where(x => x.Profession == (Profession)Enum.Parse(typeof(Profession), "Lawyer")).To<AllLawyersModel>().ToListAsync();
            return result.OrderByDescending(x => x.AverageGrade / x.ReviewsCount);
        }

        public async Task<IEnumerable<NotaryViewModel>> SearchNotaryAsync(string? townName)
        {
            if (String.IsNullOrEmpty(townName))
            {
                return await this.companyRepository.All().Where(x => x.StopAccount == false).Where(x => x.Profession == (Profession)Enum.Parse(typeof(Profession), "Notary")).To<NotaryViewModel>().ToListAsync();
            }
            return await this.companyRepository.All().Where(x => x.StopAccount == false).Where(x => x.Town.Name.ToLower() == townName.ToLower()).Where(x => x.Profession == (Profession)Enum.Parse(typeof(Profession), "Notary")).To<NotaryViewModel>().ToListAsync();
        }
    }
}