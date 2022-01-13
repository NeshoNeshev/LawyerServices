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

        private readonly IDeletableEntityRepository<Town> townRepository;

        public SearchService(IDeletableEntityRepository<AreasOfActivity> areaRepository, IDeletableEntityRepository<Company> companyRepository, IDeletableEntityRepository<Town> townRepository)
        {
            this.areaRepository = areaRepository;
            this.companyRepository = companyRepository;
            this.townRepository = townRepository;
        }
        public IEnumerable<LawyersAreaOfActivityViewModel> SearchAllLawyersByAreaId(string areaId)
        {
            var query = this.areaRepository.All().Where(c => c.Id == areaId).SelectMany(x => x.AreasCompanies, (x, c) => new { x, c })
                .Select(xc => new
                    {
                            xc.x.Id,
                            xc.x.Name,
                            xc.c.AreasOfActivity,
                            xc.c.Company
                        }).ToList();

            var allLawyers = new List<LawyersAreaOfActivityViewModel>();

            foreach (var item in query)
            {
                allLawyers.Add(new LawyersAreaOfActivityViewModel()
                {
                    Name = item.Name,
                    Id = item.Id,
                    Area = item.AreasOfActivity.Name,
                    Company = item.Company.FirstName

                });
            }
            return allLawyers;
        }

        public IEnumerable<T> SearchAllLawyersByTown<T>(string townId)
        {
            IQueryable<Company> query = this.companyRepository.All().Where(x => x.Profession == (Profession)Enum.Parse(typeof(Profession), "Lawyer")).Where(l => l.TownId == townId);

            return query.To<T>().ToList();
        }

        public IEnumerable<LawyerListItem> Search(string? name, string? townId, string? areaId)
        {
            var town = new List<LawyerListItem>();
            town = companyRepository.All().Where(x => x.TownId == townId).To<LawyerListItem>().ToList();


            if (!String.IsNullOrEmpty(townId))
            {
               
            }
            else if (townId == null)
            {

            }
            return null;
        }


    }
}
