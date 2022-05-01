using LaweyrServices.Web.Shared.FixedCostModels;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using LawyerServices.Services.Mapping;

namespace LawyerServices.Services.Data
{
    public class FixedPriceService : IFixedPriceService
    {
        private readonly IDeletableEntityRepository<FixedCostService> fixedCost;

        public FixedPriceService(IDeletableEntityRepository<FixedCostService> fixedCost)
        {
            this.fixedCost = fixedCost;
        }

        public async Task<string> CreateServiceAsyns(FixedCostInputModel model)
        {
            var service = new FixedCostService()
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                Price = model.Price,
                CompanyId = model.lawyerId,

            };
            await this.fixedCost.AddAsync(service);
            await this.fixedCost.SaveChangesAsync();
            return service.Id;
        }
        public async Task DeleteServiceAsync(string serviceId)
        {
            var service = this.fixedCost.All().Where(s => s.Id == serviceId).FirstOrDefault();
            if (service == null)
            {
                return;
            }
            this.fixedCost.Delete(service);
            await this.fixedCost.SaveChangesAsync();

        }
        public IEnumerable<T> GetAll<T>(string lawyerId)
        {
            IQueryable<FixedCostService> query = this.fixedCost.All().Where(x => x.CompanyId == lawyerId).OrderBy(x => x.Price);

            return query.To<T>().ToList();
        }

        public async Task UpdateFixedCostServiceAsync(FixedCostUpdateModel model)
        {
            var result = this.fixedCost.All().FirstOrDefault(f => f.Id == model.FixedCostId);
            if (result != null)
            {
                result.Name = model.Name;
                result.Price = model.Price;

                this.fixedCost.Update(result);
               await this.fixedCost.SaveChangesAsync();
            }

        }
    }
}
