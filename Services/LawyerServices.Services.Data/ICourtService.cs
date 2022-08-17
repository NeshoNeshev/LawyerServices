using LaweyrServices.Web.Shared.CourtModels;

namespace LawyerServices.Services.Data
{
    public interface ICourtService
    {
        public Task CreateCourt(CourtInputModel model);

        public Task<IEnumerable<T>> GetCourtsAsync<T>();

        public Task<IEnumerable<string>> GeAlltCourtsAsync(IEnumerable<string> courtsNames);
    }
}
