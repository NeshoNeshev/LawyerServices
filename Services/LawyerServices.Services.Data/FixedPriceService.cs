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

        public async Task<string> CreateService(FixedCostInputModel model)
        {
            var service = new FixedCostService()
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                Price = model.Price,
                CompanyId = model.lawyerId,

           };
           await this.fixedCost.AddAsync(service);
                this.fixedCost.SaveChangesAsync();
            return service.Id;
        }
        public void DeleteService(string serviceId)
        {
            var service = this.fixedCost.All().Where(s => s.Id == serviceId).FirstOrDefault();
            if (service == null)
            {
                return;
            }
            this.fixedCost.Delete(service);
            this.fixedCost.SaveChangesAsync();
        
        }
        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<FixedCostService> query = this.fixedCost.All().OrderBy(x => x.Price);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public void UpdateFixedCostService(FixedCostUpdateModel model)
        {
            var result = this.fixedCost.All().FirstOrDefault(f => f.Id == model.FixedCostId);
            if (result != null)
            {
                result.Name = model.Name;
                result.Price = model.Price;

                this.fixedCost.Update(result);
                this.fixedCost.SaveChangesAsync();
            }
           
        }
    }
}
