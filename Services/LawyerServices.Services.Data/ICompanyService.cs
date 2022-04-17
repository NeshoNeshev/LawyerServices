using LaweyrServices.Web.Shared.DateModels;
using LaweyrServices.Web.Shared.FixedCostModels;
using LaweyrServices.Web.Shared.LawyerViewModels;

namespace LawyerServices.Services.Data
{
    public interface ICompanyService
    {
        public Task<string> CreateMoreInformation(MoreInformationInputModel model, string userId);

        public MoreInformationInputModel GetMoreInformation(string companyId);

        public Task ChangeName(string companyId, string Name);

        public Task SaveCompanyAppointments(Appointment appointment, string userId);

        public IList<Appointment> GetAllAppointments(string userId);

        public Task EditAppointment(Appointment model);

        public int UsersCount(string userId);

        public int UsersTodayCount(string userId);

        public string GetCompanyId(string userId);

        public void UpdateFeatures(FeaturesInputModel model, string lawyerid);

        public FeaturesInputModel GetFeatures(string lawyerid);

        public IList<AppointmentViewModel> GetAllAppointmentsByCurrentDate(string date, string lawyerId);
    }
}
