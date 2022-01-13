using LawyerServices.Common.LawyerViewModels;

namespace LawyerServices.Services.Data.AdminServices.AreasOfActivityServices
{
    public interface IAreasOfActivityService
    {
        public IEnumerable<T> GetAll<T>(int? count = null);
        public Task CreateArea(string userId, string companyId, string copyright);
    }
}
