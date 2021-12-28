using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;

namespace LawyerServices.Services.Data
{
    public class TownService : ITownService
    {
        private readonly IDeletableEntityRepository<Town> townRepository;

        public TownService(IDeletableEntityRepository<Town> townRepository)
        {
            this.townRepository = townRepository;
        }


        public async Task<IEnumerable<Town>> GetAll(string id)
        { 
           var towns = this.townRepository.All().Where(x => x.CountryId == id).ToList();

            return towns;
        }
    }
}
