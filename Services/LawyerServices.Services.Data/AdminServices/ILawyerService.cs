using LaweyrServices.Web.Shared.AdministratioInputModels;
using LaweyrServices.Web.Shared.DateModels;
using LaweyrServices.Web.Shared.LawyerViewModels;
using Microsoft.AspNetCore.Components.Forms;

namespace LawyerServices.Services.Data.AdminServices
{
    public interface ILawyerService
    {
        public Task<string> CreateLawyerAsync(CreateLawyerModel lawyerModel);

        public Task<bool> IsOwner(string lawyerId);
        public Task<bool> ExistingLawyerByPhoneAsync(string phoneNumber);

        public Task<bool> ExistingLawyerByEmail(string email);

        public IEnumerable<T> GetAllLawyers<T>(int? count = null);

        public IEnumerable<T> SearchAllLawyersByTownAndCategory<T>(string townId);

        public Task<LawyerListItem> GetLawyerByIdAsync(string userId);

        public Task<string> EditImageAsync(byte[] bytes, string userId, string extension);

        public Task UpdateLawyerImageAsync(string userId, string imgPath);

        public Task<AppointmentViewModel> GetLawyerWorkingTimeExteption(string appointmentId);

        public bool ExistingLawyerById(string lawyerId);

        public Task<string> AddLawyerToLawFirmAsync(string lawFirmId, string lawyerId);

        public Task EditLawyerByAdministratorAsync(EditLawyerModel inputModel);

        public Task DeleteLawyer(string lawyerId);

        public Task RestoreAccount(string lawyerId);

        public Task<T> GetLawyerAsync<T>(string Id);
        public IEnumerable<T> GetAllLawyersByAdministrator<T>(int? count = null);

    }
}