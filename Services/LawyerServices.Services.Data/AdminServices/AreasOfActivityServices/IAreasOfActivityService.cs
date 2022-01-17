using LawyerServices.Common.AreasOfActivityViewModels;

namespace LawyerServices.Services.Data.AdminServices.AreasOfActivityServices
{
    public interface IAreasOfActivityService
    {
        public IEnumerable<T> GetAll<T>(int? count = null);
        public Task CreateArea(string companyId, AreasOfActivityInputModel areaModel);
    }
}