using LaweyrServices.Web.Shared.AdministratioInputModels;
using LaweyrServices.Web.Shared.DateModels;
using LaweyrServices.Web.Shared.LawyerViewModels;
using Microsoft.AspNetCore.Components.Forms;

namespace LawyerServices.Services.Data.AdminServices
{
    public interface ILawyerService
    {
        public Task<string> CreateLawyerAsync(CreateLawyerModel lawyerModel);

        public Task<string> ExistingLawyerByPhoneAsync(string phoneNumber);

        public bool ExistingLawyerByEmail(string email);

        public IEnumerable<T> GetAllLawyers<T>(int? count = null);

        public IEnumerable<T> SearchAllLawyersByTownAndCategory<T>(string townId);

        public LawyerListItem GetLawyerById(string userId);

        public Task EditImageAsync(byte[] bytes, string userId, string extension);

        public Task UpdateLawyerImageAsync(string userId, string imgPath);

        public AppointmentViewModel GetLawyerWorkingTimeExteption(string appointmentId);

        public bool ExistingLawyerById(string lawyerId);

        public Task<string> AddLawyerToLawFirmAsync(string lawFirmId, string lawyerId);

        public Task EditLawyerByAdministratorAsync(EditLawyerModel inputModel);

    }
}