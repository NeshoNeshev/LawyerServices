using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using LawyerServices.Services.Mapping;
using Microsoft.EntityFrameworkCore;

namespace LawyerServices.Services.Data
{
    public class TownService : ITownService
    {
        private readonly IDeletableEntityRepository<Town> townRepository;

        public TownService(IDeletableEntityRepository<Town> townRepository)
        {
            this.townRepository = townRepository;
        }


        public async Task<IEnumerable<T>> GetAll<T>(int? count = null)
        {

            IQueryable<Town> query = this.townRepository.All().OrderBy(x => x.Name);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }
            var result = await query.To<T>().ToListAsync();
            return result;
        }
    }
}