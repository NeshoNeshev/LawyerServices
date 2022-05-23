using LaweyrServices.Web.Shared.LawyerViewModels;
using LaweyrServices.Web.Shared.NotaryModels;

namespace LawyerServices.Services.Data
{
    public interface ISearchService
    {

        public IEnumerable<T> SearchAllLawyersByTown<T>(string townId);

        public Task<IEnumerable<AllLawyersModel>> SearchAsync(string? name, string? townId, string? areaId);

        public IEnumerable<LawyerListItem> SearchAllLawyersByArea(string areaId);

        public Task<IEnumerable<NotaryViewModel>> SearchNotaryAsync(string? townName);
    }
}
