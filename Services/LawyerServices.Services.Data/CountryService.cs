using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using LawyerServices.Services.Mapping;

namespace LawyerServices.Services.Data
{
    public class CountryService : ICountryService
    {
        private readonly IDeletableEntityRepository<Country> countryRepository;
        public CountryService(IDeletableEntityRepository<Country> countryRepository)
        {
            this.countryRepository = countryRepository;
        }
        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Country> query = this.countryRepository.All().OrderBy(x => x.Name);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

    }
}
