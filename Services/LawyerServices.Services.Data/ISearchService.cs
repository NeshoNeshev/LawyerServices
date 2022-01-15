using LawyerServices.Common.LawyerViewModels;

namespace LawyerServices.Services.Data
{
    public interface ISearchService
    {

        public IEnumerable<T> SearchAllLawyersByTown<T>(string townId);

        public Task<IEnumerable<LawyerListItem>> Search(string? name, string? townId, string? areaId);

        public IEnumerable<LawyerListItem> SearchAllLawyersByArea(string areaId);
    }
}
