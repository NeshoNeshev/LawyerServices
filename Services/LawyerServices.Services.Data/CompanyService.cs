﻿using LaweyrServices.Web.Shared.DateModels;
using LaweyrServices.Web.Shared.FixedCostModels;
using LaweyrServices.Web.Shared.LawyerViewModels;
using LawyerServices.Common;
using LawyerServices.Data.Models;
using LawyerServices.Data.Models.Enumerations;
using LawyerServices.Data.Repositories;
using LawyerServices.Services.Mapping;
using Microsoft.EntityFrameworkCore;

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
        

        public async Task<string> CreateMoreInformationAsync(MoreInformationInputModel model, string userId)
        {
            var company = this.userRepository.All().Where(u => u.Id == userId).Select(x => x.Company).FirstOrDefault();
            if (company is null) return null;

            company.Languages = model.Languages;
            company.AboutText = model.AboutText;
            company.HeaderText = model.HeaderText;
            company.YearFirstAdmitted = model.YearFirstAdmitted;
            company.WebSite = model.WebSite;
            

            this.companyRepository.Update(company);
            await this.companyRepository.SaveChangesAsync();

            return company.Id;
        }
        public async Task<int> GetNotaryCountAsync()
        {
            var result = await this.companyRepository.All().Where(x => x.Profession == (Profession)Enum.Parse(typeof(Profession), "Notary")).CountAsync();
            return result;
        }
        public async Task<int> GetLawyersCountAsync()
        {
            var result = await this.companyRepository.All().Where(x => x.Profession == (Profession)Enum.Parse(typeof(Profession), "Lawyer")).CountAsync();
            return result;
        }
        public async Task<IEnumerable<T>> GetLawyer<T>(string id)
        {
            IQueryable<Company> query = this.companyRepository.All().Where(x=>x.Id == id);
            

            return await query.To<T>().ToListAsync();
        }
        public MoreInformationInputModel GetMoreInformation(string userId)
        {
            var company = this.userRepository.All().Where(u => u.Id == userId).Select(x => x.Company).To<MoreInformationInputModel>().FirstOrDefault();
           
            if (company == null)
            {
                return null;
            }

            return company;
        }

        public async Task ChangeName(string companyId, string Name)
        {
            var company = await this.companyRepository.All().FirstOrDefaultAsync(c => c.Id == companyId);
            if (company is null) return;
        }
        public async Task SaveCompanyAppointmentsAsync(Appointment appointment, string lawyerId)
        {

            var companyWorkingTime = await this.companyRepository.All().Where(x => x.Id == lawyerId).Select(x => x.WorkingTime).FirstOrDefaultAsync();
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

                    }); ;
                    date = endTime;
                }
                if (workingTimes.Count > 0)
                {
                    foreach (var wt in workingTimes)
                    {
                        await this.workingTimeExceptionRepository.AddAsync(wt);
                    }
                    await this.workingTimeExceptionRepository.SaveChangesAsync();
                }
            }
            else if (appointment.Text == GlobalConstants.AnotherConsultation)
            {
                await this.workingTimeExceptionRepository.AddAsync(new WorkingTimeException()
                {
                    Id = appointment.Id,
                    WorkingTimeId = companyWorkingTime.Id,
                    StarFrom = appointment.Start.Value,
                    Date = appointment.Start.Value.Date,
                    EndTo = appointment.End.Value,
                    AppointmentType = appointment.Text,                  
                    MoreInformation = appointment.MoreInformation,                  
                    FirstName = appointment.FirstName,
                    LastName = appointment.LastName,
                    PhoneNumber = appointment.PhoneNumber,
                    Email = appointment.Email,
                    IsRequested = true,
                    IsCanceled = false,

                });
                await this.workingTimeExceptionRepository.SaveChangesAsync();
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
                    SideCase = appointment.SideCase,
                    TypeOfCase = appointment.TypeOfCase,
                    FirstName = appointment.FirstName,
                    PhoneNumber = appointment.PhoneNumber,
                    Email = appointment.Email,

                });
                await this.workingTimeExceptionRepository.SaveChangesAsync();
            }
        }


        public async Task<IList<Appointment>> GetAllAppointmentsAsync(string userId)
        {
            var allAppointments = await this.userRepository.All().Where(u => u.Id == userId).Select(x => x.Company).Select(x => x.WorkingTime).Select(x => x.WorkingTimeExceptions).FirstOrDefaultAsync();
            var appointments = new List<Appointment>();
            foreach (var exception in allAppointments)
            {
                var name = exception.FirstName + " " + exception.LastName;
                appointments.Add(new Appointment()
                {
                    
                    Id = exception.Id,
                    Start = exception.StarFrom,
                    End = exception.EndTo,
                    Court = exception.Court,
                    CaseNumber = exception.CaseNumber,
                    MoreInformation = exception.MoreInformation,
                    Text = exception.AppointmentType,
                    TypeOfCase = exception.TypeOfCase,
                    SideCase = exception.SideCase,
                    IsCanceled = exception.IsCanceled,
                    IsRequested = exception.IsRequested,
                    FirstName = name,
                    PhoneNumber = exception.PhoneNumber,
                    Email = exception.Email,
                }); ;
            }

            return appointments;
        }
        public async Task EditAppointmentAsync(Appointment model)
        {
            var appointment = this.workingTimeExceptionRepository.All().FirstOrDefault(a => a.Id == model.Id);
            if (appointment is null)
            {
                throw new AggregateException("Appointment can not be null");
            }
            
            appointment.StarFrom = model.Start.Value;
            appointment.EndTo = model.End.Value;
            appointment.Court = model.Court;
            appointment.CaseNumber = model.CaseNumber;
            appointment.SideCase = model.SideCase;
            appointment.TypeOfCase = model.TypeOfCase;
            appointment.MoreInformation = model.MoreInformation;
            appointment.FirstName = model.FirstName;
            appointment.PhoneNumber = model.PhoneNumber;
            appointment.Email = model.Email;
            appointment.AppointmentType = model.Text;
            this.workingTimeExceptionRepository.Update(appointment);
            await this.workingTimeExceptionRepository.SaveChangesAsync();

        }
        public int UsersCount(string userId)
        {
            var count = this.userRepository.All().Where(u => u.Id == userId).Select(x => x.Company).Select(x => x.WorkingTime).Select(x => x.WorkingTimeExceptions.Where(x => x.IsRequested).Count()).FirstOrDefault();
            return count;
        }
        public int UsersTodayCount(string userId)
        {
            var count = this.userRepository.All().Where(u => u.Id == userId).Select(x => x.Company).Select(x => x.WorkingTime).Select(x => x.WorkingTimeExceptions.Where(x => x.IsRequested).Where(x=>x.Date == DateTime.UtcNow).Count()).FirstOrDefault();
            return count;
        }

        public async Task<string> GetCompanyIdAsync(string userId)
        {
            var company = await this.userRepository.All().Where(u => u.Id == userId).Select(x => x.Company).FirstOrDefaultAsync();

            return company.Id;
        }

        public FeaturesInputModel GetFeatures(string lawyerid)
        {
            var lawyer = this.companyRepository.All().FirstOrDefault(x => x.Id == lawyerid);

            var model = new FeaturesInputModel();
            model.NoWinNoFee = (bool)lawyer?.NoWinNoFee;
            model.FixedCost = (bool)lawyer?.FixedCost;
            model.FreeFirstAppointment = (bool)(lawyer?.FreeFirstAppointment);
            model.MeetingClientLocation = (bool)(lawyer?.MeetingClientLocation);

            return model;
        }

        public async Task UpdateFeaturesAsync(FeaturesInputModel model, string lawyerid)
        {
            var lawyer = this.companyRepository.All().FirstOrDefault(x=>x.Id == lawyerid);
            if (lawyer != null)
            {
                lawyer.FreeFirstAppointment = model.FreeFirstAppointment;
                lawyer.FixedCost = model.FixedCost;
                lawyer.NoWinNoFee = model.NoWinNoFee;
                lawyer.MeetingClientLocation = model.MeetingClientLocation;

                this.companyRepository.Update(lawyer);
                await this.companyRepository.SaveChangesAsync();
            }
        
        }

        public IList<AppointmentViewModel> GetAllAppointmentsByCurrentDate(DateTime date, string lawyerId)
        {
           
           var wte = this.companyRepository.All().Where(x => x.Id == lawyerId).Select(x => x.WorkingTime).Select(x => x.WorkingTimeExceptions).FirstOrDefault().Where(x=>x.Date.Date == date.Date);
            var appointments = new List<AppointmentViewModel>();
            foreach (var exception in wte)
            {
                appointments.Add(new AppointmentViewModel()
                {
                    StarFrom = exception.StarFrom,
                    EndTo = exception.EndTo,
                    Date = exception.Date,
                });
            }
            return appointments;
        }
        public IList<Appointment> GetAllAppointmentsByLawyerId(string lawyerId)
        {
            var allAppointments = this.companyRepository.All().Where(u => u.Id == lawyerId).Select(x => x.WorkingTime).Select(x => x.WorkingTimeExceptions).FirstOrDefault();

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
                    FirstName = exception.FirstName,
                    PhoneNumber = exception.PhoneNumber,
                    Email = exception.Email,
                    CaseNumber = exception.CaseNumber,
                    MoreInformation = exception.MoreInformation,
                    Text = exception.AppointmentType,
                    TypeOfCase = exception.TypeOfCase,
                    SideCase = exception.SideCase,
                    IsCanceled = exception.IsCanceled,
                    IsRequested = exception.IsRequested,
                });
            }

            return appointments;
        }
        public async Task StopAccountAsync(string id)
        {
            var company = await this.companyRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            var user = await this.userRepository.All().FirstOrDefaultAsync(x=>x.CompanyId == company.Id);

            company.StopAccount = true;
            user.IsBan = true;



            this.companyRepository.Update(company);
            await this.companyRepository.SaveChangesAsync();

            this.userRepository.Update(user);
            await this.userRepository.SaveChangesAsync();
        }
        public async Task ActivateAccountAsync(string id)
        {
            var company = await this.companyRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            var user =   await this.userRepository.All().FirstOrDefaultAsync(x => x.CompanyId == company.Id);

            company.StopAccount = false;
            user.IsBan = false;



            this.companyRepository.Update(company);
            await this.companyRepository.SaveChangesAsync();

            this.userRepository.Update(user);
            await this.userRepository.SaveChangesAsync();
        }

        public async Task<int> GetUsersInCompanyCountAsync(string lawyerId)
        {
            var wte = await  this.companyRepository.All().Where(x => x.Id == lawyerId).Select(x=>x.WorkingTime).SelectMany(x=>x.WorkingTimeExceptions).Where(x=>x.IsRequested == true && x.IsCanceled == false).ToListAsync();
            var count = wte.DistinctBy(x => x.UserId);
            if (count == null)
            {
                return 0;
            }
            else
            {
                return count.Count();
            }          
        }
    }
}
