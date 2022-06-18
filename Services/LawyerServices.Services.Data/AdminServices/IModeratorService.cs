using LaweyrServices.Web.Shared.UserModels;

namespace LawyerServices.Services.Data.AdminServices
{
    public interface IModeratorService
    {
        public Task CreateModerator(ModeratorInputModel model);

        public Task<string> GetModeratorId(string userId);
    }
}
