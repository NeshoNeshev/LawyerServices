using LawyerServices.Common.LawyerViewModels;
using LawyerServices.Shared.AdministrationInputModels;
using Microsoft.AspNetCore.Components.Forms;

namespace LawyerServices.Services.Data.AdminServices
{
    public interface ILawyerService
    {
        public Task CreateLawyer(CreateLawyerModel lawyerModel);

        public  Task<string> ExistingLawyerByPhone(string phoneNumber);

        public bool ExistingLawyerByEmail(string email);

        public IEnumerable<T> GetAllLawyers<T>(int? count = null);

        public IEnumerable<T> SearchAllLawyersByTownAndCategory<T>(string townId);

        public LawyerListItem GetLawyerById(string userId);

        public Task EditImage(InputFileChangeEventArgs args, string userId);
    }
}