using LawyerServices.Common.LawyerViewModels;

namespace LawyerServices.Services.Data
{
    public interface ISearchService
    {
        public IEnumerable<LawyersAreaOfActivityViewModel> SearchAllLawyersByAreaId(string areaId);

        public IEnumerable<T> SearchAllLawyersByTown<T>(string townId);

        public IEnumerable<LawyerListItem> Search(string? name, string? townId, string? areaId);


    }
}
