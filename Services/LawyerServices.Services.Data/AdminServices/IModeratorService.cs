using LaweyrServices.Web.Shared.UserModels;

namespace LawyerServices.Services.Data.AdminServices
{
    public interface IModeratorService
    {
        public Task CreateModerator(ModeratorInputModel model);

        public Task<string> GetModeratorId(string userId);

        public Task StopAccountAsync(string id);

        public Task RestoreAccountAsync(string id);

        public Task<IEnumerable<T>> GetAllModerators<T>();
    }
}
