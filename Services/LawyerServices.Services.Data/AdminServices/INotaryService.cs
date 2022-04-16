using LaweyrServices.Web.Shared.AdministratioInputModels;
using LaweyrServices.Web.Shared.NotaryModels;

namespace LawyerServices.Services.Data.AdminServices
{
    public interface INotaryService
    {
        public Task<string> CreateNotary(CreateNotaryModel notaryModel);

        public IEnumerable<T> GetAllNotary<T>(int? count = null);

        public  Task EditNotaryByAdministrator(EditNotaryModel inputModel);
    }
}
