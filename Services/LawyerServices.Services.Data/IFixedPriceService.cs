using LaweyrServices.Web.Shared.FixedCostModels;

namespace LawyerServices.Services.Data
{
    public interface IFixedPriceService
    {
        public Task<string> CreateServiceAsyns(FixedCostInputModel model);

        public IEnumerable<T> GetAll<T>(string lawyerId);

        public Task DeleteServiceAsync(string serviceId);

        public Task UpdateFixedCostServiceAsync(FixedCostUpdateModel model);
    }
}
