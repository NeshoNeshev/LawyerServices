using LaweyrServices.Web.Shared.ProfileModels;

namespace LawyerServices.Services.Data
{
    public interface ICurrentProfileService
    {
        public LawyerProfileViewModel GetLawyerProfileInformation(string Id);

        public Task EditLawyerProfileInformationAsync(EditLawyerProfileModel model);
    }
}
