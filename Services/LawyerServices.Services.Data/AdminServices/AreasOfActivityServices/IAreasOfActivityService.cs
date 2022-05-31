using LaweyrServices.Web.Shared.AreasOfActivityViewModels;

namespace LawyerServices.Services.Data.AdminServices.AreasOfActivityServices
{
    public interface IAreasOfActivityService
    {
        public Task<IEnumerable<AreasOfActivityViewModel>> GetAllAreasByCompanyId(string userId);

        public Task<IEnumerable<T>> GetAll<T>(int? count = null);

        public Task DeleteAreaAsync(string areaId);

        public Task CreateAreasAsync(string areaName, string lawyerId);

    }
}