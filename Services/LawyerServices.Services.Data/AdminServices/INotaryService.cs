using LaweyrServices.Web.Shared.NotaryModels;

namespace LawyerServices.Services.Data.AdminServices
{
    public interface INotaryService
    {
        public Task<string> CreateNotary(CreateNotaryModel notaryModel);
    }
}
