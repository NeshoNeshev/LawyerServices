using LawyerServices.Common.DateModels;
using LawyerServices.Common.LawyerViewModels;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using LawyerServices.Services.Mapping;

namespace LawyerServices.Services.Data
{
    public class CompanyService : ICompanyService
    {
        private readonly IDeletableEntityRepository<Company> companyRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<WorkingTimeException> workingTimeExceptionRepository;
        public CompanyService(IDeletableEntityRepository<Company> companyREpository, IDeletableEntityRepository<ApplicationUser> userRepository, IDeletableEntityRepository<WorkingTimeException> workingTimeExceptionRepository)
        {
            this.companyRepository = companyREpository;
            this.userRepository = userRepository;
            this.workingTimeExceptionRepository = workingTimeExceptionRepository;
        }

        public async Task<string> CreateMoreInformation(MoreInformationInputModel model, string userId)
        {
            var company = this.userRepository.All().Where(u => u.Id == userId).Select(x => x.Company).FirstOrDefault();
            if (company is null) return null;

            company.Languages = model.Languages;
            company.Education = model.Education;
            company.Qualifications = model.Qualifications;
            company.Experience = model.Experience;
            company.WebSite = model.WebSite;
            company.ImgUrl = model.ImgUrl;

            this.companyRepository.Update(company);
            this.companyRepository.SaveChangesAsync();

            return company.Id;
        }

        public MoreInformationInputModel GetMoreInformation(string userId)
        {
            var company = this.userRepository.All().Where(u => u.Id == userId).Select(x => x.Company).To<MoreInformationInputModel>().FirstOrDefault();
            //var model = this.companyRepository.All().Where(x => x.Id == companyId).To<MoreInformationViewModel>().FirstOrDefault();
            if (company == null)
            {
                //todo: change
                return null;
            }

            return company;
        }

        public async Task ChangeName(string companyId, string Name)
        {
            var company = this.companyRepository.All().FirstOrDefault(c => c.Id == companyId);
            if (company is null) return;
        }
        public async Task SaveCompanyAppointments(IList<Appointment> appointments, string userId)
        {


            var companyWorkingTime = this.userRepository.All().Where(x => x.Id == userId).Select(x => x.Company).Select(x => x.WorkingTime).FirstOrDefault();
            if (companyWorkingTime is null) return;
            if (appointments.Count == 0) return;
            var timeToSave = new List<WorkingTimeException>();

            foreach (var appointment in appointments)
            {
                if (appointment.Id is null)
                {
                    continue;
                }
                timeToSave.Add(new WorkingTimeException()
                {
                    Id = appointment.Id,
                    UserId = userId,
                    WorkingTimeId = companyWorkingTime.Id,
                    StarFrom = appointment.Start,
                    EndTo = appointment.End,
                    AppointmentType = appointment.Text,
                    Court = appointment.Court,
                    MoreInformation = appointment.MoreInformation,
                    CaseNumber = appointment.CaseNumber,

                });
            }
            foreach (var time in timeToSave)
            {
                await this.workingTimeExceptionRepository.AddAsync(time);
            }
            this.workingTimeExceptionRepository.SaveChangesAsync();


        }
        public IList<Appointment> GetAllAppointments(string userId)
        {
            var appointment = this.userRepository.All().Where(x => x.Id == userId).Select(x => x.WorkingTimeExceptions).FirstOrDefault();

            var appointments = new List<Appointment>();
            foreach (var exception in appointment)
            {
                appointments.Add(new Appointment()
                {
                    Id = exception.Id,
                    Start = exception.StarFrom,
                    End = exception.EndTo,
                    Court= exception.Court,
                    CaseNumber = exception.CaseNumber,
                    MoreInformation = exception.MoreInformation,
                    Text = exception.AppointmentType,

                });
            }

            return appointments;
        }
        public void EditAppointment(Appointment model)
        {
            var appointment = this.workingTimeExceptionRepository.All().FirstOrDefault(a => a.Id == model.Id);
            if (appointment is null)
            {
                throw new AggregateException("Appointment can not be null");
            }
            appointment.StarFrom = model.Start;
            appointment.EndTo = model.End;
            this.workingTimeExceptionRepository.Update(appointment);
            this.workingTimeExceptionRepository.SaveChangesAsync();

        }
    }
}
