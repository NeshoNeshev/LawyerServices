using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using LawyerServices.Services.Mapping;

namespace LawyerServices.Services.Data.AdminServices.AreasOfActivityServices
{
    public class AreasOfActivityService : IAreasOfActivityService
    {
        private readonly IDeletableEntityRepository<AreasOfActivity> areaRepository;

        public AreasOfActivityService(IDeletableEntityRepository<AreasOfActivity> areaRepository)
        {
            this.areaRepository = areaRepository;
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
    }
}

