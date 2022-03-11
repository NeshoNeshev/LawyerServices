using LaweyrServices.Web.Shared.AreasOfActivityViewModels;

namespace LawyerServices.Services.Data.AdminServices.AreasOfActivityServices
{
    public interface IAreasOfActivityService
    {
        public IEnumerable<string> GetAllAreasByCompanyId(string userId);

        public IEnumerable<T> GetAll<T>(int? count = null);

        public Task CreateArea(string companyId, AreasOfActivityInputModel areaModel);

        public Task CreateAreas(IList<string> areas, string userId);

        public IEnumerable<AreasOfActivityViewModel> AllAreas();
    }
}