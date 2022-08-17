using LaweyrServices.Web.Shared.DateModels;
using LaweyrServices.Web.Shared.FixedCostModels;
using LaweyrServices.Web.Shared.LawyerViewModels;

namespace LawyerServices.Services.Data
{
    public interface ICompanyService
    {
        public Task<int> GetLawyersCountAsync();

        public Task<int> GetNotaryCountAsync();

        public Task<int> GetUsersInCompanyCountAsync(string lawyerId);

        public Task<string> CreateMoreInformationAsync(MoreInformationInputModel model, string userId);

        public IList<Appointment> GetAllAppointmentsByLawyerId(string lawyerId);

        public MoreInformationInputModel GetMoreInformation(string companyId);

        public Task ChangeName(string companyId, string Name);

        public Task SaveCompanyAppointmentsAsync(Appointment appointment, string userId);

        public Task<IList<Appointment>> GetAllAppointmentsAsync(string userId);

        public Task EditAppointmentAsync(Appointment model);

        public int UsersCount(string userId);

        public int UsersTodayCount(string userId);

        public Task<string> GetCompanyIdAsync(string userId);

        public Task UpdateFeaturesAsync(FeaturesInputModel model, string lawyerid);

        public FeaturesInputModel GetFeatures(string lawyerid);

        public IList<AppointmentViewModel> GetAllAppointmentsByCurrentDate(DateTime date, string lawyerId);

        public Task ActivateAccountAsync(string id);

        public Task StopAccountAsync(string id);

        public Task<IEnumerable<T>> GetLawyer<T>(string id);

    }
}
