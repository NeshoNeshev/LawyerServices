using LaweyrServices.Web.Shared.FixedCostModels;

namespace LawyerServices.Services.Data
{
    public interface IFixedPriceService
    {
        public Task<string> CreateService(FixedCostInputModel model);

        public IEnumerable<T> GetAll<T>(int? count = null);

        public void DeleteService(string serviceId);

        public void UpdateFixedCostService(FixedCostUpdateModel model);
    }
}
