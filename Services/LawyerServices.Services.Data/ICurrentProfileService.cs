using LaweyrServices.Web.Shared.ProfileModels;

namespace LawyerServices.Services.Data
{
    public interface ICurrentProfileService
    {
        public Task<LawyerProfileViewModel> GetLawyerProfileInformationAsync(string Id);

        public Task EditLawyerProfileInformationAsync(EditLawyerProfileModel model);
    }
}
