using LawyerServices.Shared.AdministrationInputModels;

namespace LawyerServices.Services.Data.AdminServices
{
    public interface ILawyerService
    {
        public Task CreateLawyer(CreateLawyerModel lawyerModel);

        public bool ExistingLawyerByPhone(string phoneNumber);

        public bool ExistingLawyerByEmail(string email);

        public IEnumerable<T> GetAllLawyers<T>(int? count = null);
    }
}
