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

        public async Task<string> CreateMoreInformation(string languages, string education, string qualifications, string experience, string website, string companyId, string path)
        {
            var company = this.companyRepository.All().FirstOrDefault(c => c.Id == companyId);
            if (company is null) return null;

            company.Languages = languages;
            company.Education = education;
            company.Qualifications = qualifications;
            company.Experience = experience;
            company.WebSite = website;
            company.ImgUrl = path;

            this.companyRepository.Update(company);
            this.companyRepository.SaveChangesAsync();

            return company.Id;
        }

        public MoreInformationViewModel GetMoreInformation(string companyId)
        {
            var model = this.companyRepository.All().Where(x => x.Id == companyId).To<MoreInformationViewModel>().FirstOrDefault();
            if (model == null)
            {
                //todo: change
                return null;
            }

            return model;
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
                timeToSave.Add(new WorkingTimeException()
                {
                    Id = appointment.Id,
                    WorkingTimeId = companyWorkingTime.Id,
                    StarFrom = appointment.Start,
                    EndTo = appointment.End,
                    UserId = userId,

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
            var user = this.userRepository.All().Where(x => x.Id == userId).Select(x=>x.WorkingTimeExceptions).FirstOrDefault();
            //var exceptions = user.WorkingTimeExceptions.ToList();
            var appointments = new List<Appointment>();
            foreach (var exception in user)
            {
                appointments.Add(new Appointment()
                {
                    Id = exception.Id,
                    Start = exception.StarFrom,
                    End = exception.EndTo,

                });
            }

            return appointments;
        }
        public void EditAppointment(Appointment model)
        {
            var appointment = this.workingTimeExceptionRepository.All().FirstOrDefault(a=>a.Id == model.Id);
            appointment.StarFrom = model.Start;
            appointment.EndTo = model.End;
            this.workingTimeExceptionRepository.Update(appointment);
            this.workingTimeExceptionRepository.SaveChangesAsync();
        }
    }
}
