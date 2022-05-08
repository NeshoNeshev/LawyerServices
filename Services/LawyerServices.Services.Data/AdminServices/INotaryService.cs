using LaweyrServices.Web.Shared.AdministratioInputModels;
using LaweyrServices.Web.Shared.NotaryModels;

namespace LawyerServices.Services.Data.AdminServices
{
    public interface INotaryService
    {
        public Task<string> CreateNotaryAsync(CreateNotaryModel notaryModel);

        public IEnumerable<T> GetAllNotary<T>(int? count = null);

        public  Task EditNotaryByAdministratorAsync(EditNotaryModel inputModel);

        public Task<NotaryViewModel> GetNotaryById(string notaryId);

        public IEnumerable<T> GetAllNotaryByAdministrator<T>(int? count = null);

        public Task DeleteNotary(string notaryId);

        public Task<string> RestoreAccount(string notaryId);
    }
}
