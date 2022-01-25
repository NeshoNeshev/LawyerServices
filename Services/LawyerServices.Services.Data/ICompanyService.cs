using LawyerServices.Common.DateModels;
using LawyerServices.Common.LawyerViewModels;

namespace LawyerServices.Services.Data
{
    public interface ICompanyService
    {
        public Task<string> CreateMoreInformation(string languages, string education, string qualifications, string experience,string website, string companyId, string path);

        public MoreInformationViewModel GetMoreInformation(string companyId);

        public Task ChangeName(string companyId, string Name);

        public Task SaveCompanyAppointments(IList<Appointment> appointments, string userId);

        public IList<Appointment> GetAllAppointments(string userId);

        public void EditAppointment(Appointment model);
    }
}
