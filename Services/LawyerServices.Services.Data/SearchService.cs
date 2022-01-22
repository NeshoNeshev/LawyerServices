using LawyerServices.Common.LawyerViewModels;
using LawyerServices.Data.Models;
using LawyerServices.Data.Models.Enumerations;
using LawyerServices.Data.Repositories;
using LawyerServices.Services.Mapping;

namespace LawyerServices.Services.Data
{
    public class SearchService : ISearchService
    {
        private readonly IDeletableEntityRepository<AreasOfActivity> areaRepository;

        private readonly IDeletableEntityRepository<Company> companyRepository;

        private readonly IDeletableEntityRepository<AreasCompany> areaCompanyrepository;

        public SearchService(IDeletableEntityRepository<AreasOfActivity> areaRepository, IDeletableEntityRepository<Company> companyRepository, IDeletableEntityRepository<AreasCompany> areaCompanyrepository)
        {
            this.areaRepository = areaRepository;
            this.companyRepository = companyRepository;
            this.areaCompanyrepository = areaCompanyrepository;
        }
        //public IEnumerable<LawyersAreaOfActivityViewModel> SearchAllLawyersByAreaId(string areaId)
        //{
        //    var query = this.areaRepository.All().Where(c => c.Id == areaId).SelectMany(x => x.AreasCompanies, (x, c) => new { x, c })
        //        .Select(xc => new
        //            {
        //                    xc.x.Id,
        //                    xc.x.Name,
        //                    xc.c.AreasOfActivity,
        //                    xc.c.Company
        //                }).ToList();

        //    var allLawyers = new List<LawyersAreaOfActivityViewModel>();

        //    foreach (var item in query)
        //    {
        //        allLawyers.Add(new LawyersAreaOfActivityViewModel()
        //        {
        //            Name = item.Name,
        //            Id = item.Id,
        //            Area = item.AreasOfActivity.Name,
        //            Company = item.Company.FirstName

        //        });
        //    }
        //    return allLawyers;
        //}

        public IEnumerable<T> SearchAllLawyersByTown<T>(string townId)
        {
            IQueryable<Company> query = this.companyRepository.All().Where(x => x.Profession == (Profession)Enum.Parse(typeof(Profession), "Lawyer")).Where(l => l.TownId == townId);

            return query.To<T>().ToList();
        }
        public IEnumerable<LawyerListItem> SearchAllLawyersByArea(string areaId)
        {

            return this.areaCompanyrepository.All().Where(x => x.AreasOfActivity.Id == areaId).Select(x => x.Company).Where(x => x.Profession == (Profession)Enum.Parse(typeof(Profession), "Lawyer")).To<LawyerListItem>();

        }
        public async Task<IEnumerable<LawyerListItem>> Search(string? name, string? townName, string? areaName)
        {
            if (!String.IsNullOrEmpty(townName) && !String.IsNullOrEmpty(areaName))
            {
                return this.areaCompanyrepository.All().Where(x => x.AreasOfActivity.Name.ToLower() == areaName.ToLower()).Select(x => x.Company).Where(x => x.Town.Name == townName).To<LawyerListItem>().ToList();
            }
            if (!String.IsNullOrEmpty(townName))
            {
                var lawyersInTown = this.companyRepository.All().Where(x => x.Town.Name.ToLower() == townName.ToLower()).To<LawyerListItem>().ToList();
                if (lawyersInTown.Any())
                {
                    return this.companyRepository.All().Where(x => x.Town.Name.ToLower() == townName.ToLower()).To<LawyerListItem>().ToList();
                }
                else
                {
                    return this.areaCompanyrepository.All().Where(x => x.AreasOfActivity.Name.ToLower() == townName.ToLower()).Select(x => x.Company).To<LawyerListItem>().ToList();
                }
            }
            if (!String.IsNullOrEmpty(areaName))
            {
                return this.areaCompanyrepository.All().Where(x => x.AreasOfActivity.Name.ToLower() == areaName.ToLower()).Select(x => x.Company).To<LawyerListItem>().ToList();
            }
           
            return this.companyRepository.All().To<LawyerListItem>().ToList();
        }
    }
}