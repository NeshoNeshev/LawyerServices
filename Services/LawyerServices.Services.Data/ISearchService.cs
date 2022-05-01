using LaweyrServices.Web.Shared.LawyerViewModels;

namespace LawyerServices.Services.Data
{
    public interface ISearchService
    {

        public IEnumerable<T> SearchAllLawyersByTown<T>(string townId);

        public Task<IEnumerable<LawyerListItem>> SearchAsync(string? name, string? townId, string? areaId);

        public IEnumerable<LawyerListItem> SearchAllLawyersByArea(string areaId);
    }
}
