﻿using LawyerServices.Common.DateModels;
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
        public async Task SaveCompanyAppointments(Appointment appointment, string userId)
        {


            var companyWorkingTime = this.userRepository.All().Where(x => x.Id == userId).Select(x => x.Company).Select(x => x.WorkingTime).FirstOrDefault();
            if (companyWorkingTime is null) return;
            if (appointment == null) return;

            if (appointment.Id is null)
            {
                return;
            }
            if (appointment.IsChecked == true)
            {
                var start = appointment.StartDiapazone.Value;
                var date = appointment.Date.Value.Date;
                date = date.Add(appointment.StartDiapazone.Value);

                var endDate = appointment.Date.Value.Date;
                endDate = endDate.Add(appointment.EndtDiapazone.Value);

                var step = appointment.Step;
                List<WorkingTimeException> workingTimes = new List<WorkingTimeException>();

                while (true)
                {

                    var endTime = date.AddMinutes((double)step);

                    if (endTime > endDate)
                    {
                        break;
                    }

                    workingTimes.Add(new WorkingTimeException()
                    {
                        Id = Guid.NewGuid().ToString(),
                        WorkingTimeId = companyWorkingTime.Id,
                        StarFrom = date,
                        EndTo = endTime,
                        AppointmentType = appointment.Text,
                        Date = date,

                    });;
                    date = endTime;
                }
                if (workingTimes.Count > 0)
                {
                    foreach (var wt in workingTimes)
                    {
                        await this.workingTimeExceptionRepository.AddAsync(wt);
                    }
                    this.workingTimeExceptionRepository.SaveChangesAsync();
                }
            }
            else
            {
                await this.workingTimeExceptionRepository.AddAsync(new WorkingTimeException()
                {
                    Id = appointment.Id,
                    WorkingTimeId = companyWorkingTime.Id,
                    StarFrom = appointment.Start.Value,
                    Date = appointment.Start.Value.Date,
                    EndTo = appointment.End.Value,
                    AppointmentType = appointment.Text,
                    Court = appointment.Court,
                    MoreInformation = appointment.MoreInformation,
                    CaseNumber = appointment.CaseNumber,

                });
                this.workingTimeExceptionRepository.SaveChangesAsync();
            }
        }


        public IList<Appointment> GetAllAppointments(string userId)
        {
            var allAppointments = this.userRepository.All().Where(u => u.Id == userId).Select(x => x.Company).Select(x => x.WorkingTime).Select(x => x.WorkingTimeException).FirstOrDefault();
            
            //var appointment = this.userRepository.All().Where(x => x.Id == userId).Select(x => x.WorkingTimeExceptions).FirstOrDefault();
             
            var appointments = new List<Appointment>();
            foreach (var exception in allAppointments)
            {
                appointments.Add(new Appointment()
                {
                    Id = exception.Id,
                    Start = exception.StarFrom,
                    End = exception.EndTo,
                    Court = exception.Court,
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
            appointment.StarFrom = model.Start.Value;
            appointment.EndTo = model.End.Value;
            this.workingTimeExceptionRepository.Update(appointment);
            this.workingTimeExceptionRepository.SaveChangesAsync();

        }
    }
}
