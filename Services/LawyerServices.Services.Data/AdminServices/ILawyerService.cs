using LaweyrServices.Web.Shared.AdministratioInputModels;
using LaweyrServices.Web.Shared.DateModels;
using LaweyrServices.Web.Shared.LawyerViewModels;
using Microsoft.AspNetCore.Components.Forms;

namespace LawyerServices.Services.Data.AdminServices
{
    public interface ILawyerService
    {
        public Task<string> CreateLawyer(CreateLawyerModel lawyerModel);

        public Task<string> ExistingLawyerByPhone(string phoneNumber);

        public bool ExistingLawyerByEmail(string email);

        public IEnumerable<T> GetAllLawyers<T>(int? count = null);

        public IEnumerable<T> SearchAllLawyersByTownAndCategory<T>(string townId);

        public LawyerListItem GetLawyerById(string userId);

        public Task EditImage(byte[] bytes, string userId, string extension);

        public void UpdateLawyerImage(string userId, string imgPath);

        public AppointmentViewModel GetLawyerWorkingTimeExteption(string appointmentId);

        public bool ExistingLawyerById(string lawyerId);

        public Task<string> AddLawyerToLawFirm(string lawFirmId, string lawyerId);

    }
}