using LawyerServices.Services.Mapping;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using LaweyrServices.Web.Shared.WorkingTimeModels;
using LaweyrServices.Web.Shared.UserModels;
using LaweyrServices.Web.Shared.DateModels;
using LawyerServices.Common;
using Microsoft.EntityFrameworkCore;
using LawyerServices.Services.Messaging;
using System.Text;

namespace LawyerServices.Services.Data
{
    public class WorkingTimeExceptionService : IWorkingTimeExceptionService
    {
        private readonly IDeletableEntityRepository<WorkingTime> workingTimeRepository;
        private readonly IDeletableEntityRepository<Company> companyRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<WorkingTimeException> weRepository;
        private readonly IEmailSender emailSender;
        public WorkingTimeExceptionService(IDeletableEntityRepository<Company> companyRepository,
            IDeletableEntityRepository<WorkingTimeException> weRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository, IEmailSender emailSender
, IDeletableEntityRepository<WorkingTime> workingTimeRepository)
        {
            this.companyRepository = companyRepository;
            this.weRepository = weRepository;
            this.userRepository = userRepository;
            this.emailSender = emailSender;
            this.workingTimeRepository = workingTimeRepository;
        }

        public async Task SendRequestToLawyerAsync(UserRequestModel? userRequestModel)
        {
            var user = await this.userRepository.All().Where(x => x.Id == userRequestModel.UserId).FirstOrDefaultAsync();
            var company = await this.companyRepository.All().FirstOrDefaultAsync(x => x.Id == userRequestModel.CompanyId);
            var userCompany = await this.userRepository.All().Where(x => x.CompanyId == userRequestModel.CompanyId).FirstOrDefaultAsync();
            var workingTimeException = await this.weRepository.All().FirstOrDefaultAsync(ex => ex.Id == userRequestModel.WorkingTimeExceptionId);
            if (workingTimeException == null || company == null || userCompany == null || user == null) return;
           
            try
            {
                workingTimeException.IsRequested = true;
                workingTimeException.IsApproved = true;
                workingTimeException.UserId = userRequestModel.UserId;
                workingTimeException.FirstName = userRequestModel.FirstName;
                workingTimeException.LastName = userRequestModel.LastName;
                workingTimeException.PhoneNumber = userRequestModel.PhoneNumber;
                workingTimeException.Email = userRequestModel.Email;
                workingTimeException.MoreInformation = userRequestModel.MoreInformation;
                if (!company.Users.Contains(user))
                {
                    company.Users.Add(user);
                    companyRepository.Update(company);
                    await companyRepository.SaveChangesAsync();
                }

                this.weRepository.Update(workingTimeException);
                await this.weRepository.SaveChangesAsync();

                if (userRequestModel.IsReserved)
                {
                    await SendEmailToUser(company.Names, workingTimeException.StarFrom, userRequestModel.Email);
                }
                if (company.IsReminderForComing)
                {
                    await SendEmailToLawyer(userRequestModel.FirstName, userRequestModel.LastName, workingTimeException.StarFrom, "nesho1978@abv.bg");
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException("SendRequestToLawyer Error");
            }
        }

        public async Task<int> GetUserRequstsCountAsync(string lawyerId)
        {

            var count = await this.companyRepository.All().Where(l => l.Id == lawyerId)
                .Select(x => x.WorkingTime)
                .SelectMany(x => x.WorkingTimeExceptions)
                .Where(x => x.IsRequested == true && x.IsCanceled == false && x.AppointmentType == GlobalConstants.Client).CountAsync();

            return count;
        }
        public async Task DeleteNotaryAppointmentAsync(Appointment model)
        {
            var wte = await this.weRepository.All().Where(x => x.Id == model.Id).FirstOrDefaultAsync();
            if (wte == null)
            {
                return;
            }
            this.weRepository.Delete(wte);
            await this.weRepository.SaveChangesAsync();
        }
        public async Task<int> GetMeetingRequstsCountAsync(string lawyerId)
        {

            var count = await this.companyRepository.All().Where(l => l.Id == lawyerId)
                .Select(x => x.WorkingTime)
                .SelectMany(x => x.WorkingTimeExceptions)
                .Where(x => x.AppointmentType == GlobalConstants.Meeting).CountAsync();

            return count;
        }
        public async Task<WorkingTimeExceptionBookingModel> GetRequestById(string wteId)
        {

            var wtexc = await this.weRepository.All().Where(w => w.Id == wteId).Where(x => x.IsRequested == true).To<WorkingTimeExceptionBookingModel>().FirstOrDefaultAsync();

            return wtexc;
        }
        public async Task<IEnumerable<WorkingTimeExceptionBookingModel>> GetAllNotaryRequsts(string userId)
        {

            var workingTimeId = await this.userRepository.All().Where(x => x.Id == userId).Select(x => x.Company).Select(x => x.WorkingTimeId).FirstOrDefaultAsync();
            var exc = await this.weRepository.All().Where(x => x.WorkingTimeId == workingTimeId).OrderBy(x => x.StarFrom).To<WorkingTimeExceptionBookingModel>().ToListAsync();

            return exc;
        }
        public IEnumerable<WorkingTimeExceptionBookingModel> GetAllRequsts(string userId)
        {

            var workingTimeId = this.userRepository.All().Where(x => x.Id == userId).Select(x => x.Company).Select(x => x.WorkingTimeId).FirstOrDefault();
            var exc = this.weRepository.All().Where(x => x.WorkingTimeId == workingTimeId).Where(x => x.IsRequested == true && x.IsCanceled == false).To<WorkingTimeExceptionBookingModel>().ToList();

            return exc;
        }
        public async Task<IEnumerable<WorkingTimeExceptionBookingModel>> GetAllRequestsByDayOfWeekAsync(string lawyerId)
        {
            var wtExceptions = await this.companyRepository.All()
                .Where(x => x.Id == lawyerId)
                .Select(x => x.WorkingTime)
                .SelectMany(x => x.WorkingTimeExceptions)
                .Where(x => x.IsRequested == true && x.IsCanceled == false && x.StarFrom >= DateTime.Now).To<WorkingTimeExceptionBookingModel>().ToListAsync();
            return wtExceptions;
        }
        public void DeleteWorkingTimeExceptionWhenDateIsOver(string userId)
        {
            var wtex = this.userRepository.All().Where(u => u.Id == userId)
                 .Select(c => c.Company)
                 .Select(w => w.WorkingTime)
                 .Select(x => x.WorkingTimeExceptions.Where(x => x.IsRequested == false).Where(x => x.Date < DateTime.UtcNow)).FirstOrDefault();
            foreach (var item in wtex)
            {
                this.weRepository.HardDelete(item);

            }
            this.weRepository.SaveChangesAsync();
        }

        //send email
        public async Task SetIsCanceledAsync(CancelCurrentWteInputModel model, string lawyerId)
        {
            var wte = await this.weRepository.All().Where(x => x.Id == model.WteId).FirstOrDefaultAsync();
            var userReminder = await this.userRepository.All().Where(x => x.Id == wte.UserId).Select(x => x.IsReserved).FirstOrDefaultAsync();
            var lawyerNames = await this.companyRepository.All().Where(x => x.Id == lawyerId).Select(x => x.Names).FirstOrDefaultAsync();
            if (wte != null)
            {
                wte.IsApproved = false;
                wte.IsCanceled = true;
                wte.AppointmentType += " отменена";
                wte.ReasonFromCanceled = model.ReasonFromCanceled;
                this.weRepository.Update(wte);
                await this.weRepository.SaveChangesAsync();
                if (userReminder == true && lawyerNames != null && wte.Email != null)
                {
                    await CancelApointmentЕmail(lawyerNames, wte.StarFrom, model.ReasonFromCanceled, lawyerId, wte.Email);
                }
            }
        }

        public async Task<bool> SetNotSHowUpAsync(string wteId)
        {
            var wte = await this.weRepository.All().FirstOrDefaultAsync(w => w.Id == wteId);

            if (wte != null && wte.IsApproved == true)
            {
                var user = this.userRepository.All().FirstOrDefault(u => u.Id == wte.UserId);
                if (user != null)
                {
                    user.NotShowUpCount++;
                    wte.NotShowUp = true;
                }
                else
                {
                    return false;
                }

                this.weRepository.Update(wte);
                await this.weRepository.SaveChangesAsync();

                this.userRepository.Update(user);
                await this.userRepository.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<IEnumerable<WorkingTimeExceptionUserViewModel>> GetRequestsForUserIdAsync(string userId)
        {
            var wteExceptions = await this.weRepository.All().Where(w => w.UserId == userId).To<WorkingTimeExceptionUserViewModel>().OrderByDescending(x => x.StarFrom).ToListAsync();

            return wteExceptions;
        }
        //Todo send email to lawyer
        public async Task SetWorkingTimeExceptionToFreeAsync(string wteId, string userId)
        {
            var wte = await this.weRepository.All().FirstOrDefaultAsync(w => w.Id == wteId);
            var user = await this.userRepository.All().FirstOrDefaultAsync(u => u.Id == userId);
           
            wte.IsRequested = false;
            wte.IsApproved = false;
            wte.MoreInformation = null;
            wte.FirstName = null;
            wte.LastName = null;
            wte.PhoneNumber = null;
            wte.Email = null;
            wte.UserId = null;

            user.CancelledCount++;

            this.weRepository.Update(wte);
            await this.weRepository.SaveChangesAsync();

            this.userRepository.Update(user);
            await this.userRepository.SaveChangesAsync();

        }


        public async Task CancelAppointmentFromDateAsync(CancelAppointmentForOneDateInputModel model, string lawyerId)
        {
            //todo check aftermorning
            var wtExceptions = await this.companyRepository.All().Where(x => x.Id == lawyerId).Select(x => x.WorkingTime).SelectMany(x => x.WorkingTimeExceptions).Where(x => x.Date.Date == model.Date.Date).ToListAsync();
            if (wtExceptions == null) return;
            var lawyerNames = await this.companyRepository.All().Where(x => x.Id == lawyerId).Select(x => x.Names).FirstOrDefaultAsync();
           
            if (wtExceptions.Any())
            {

                foreach (var exception in wtExceptions)
                {

                    if (exception.AppointmentType.ToLower() == GlobalConstants.Meeting.ToLower())
                    {
                        continue;
                    }
                    if (exception.IsApproved == true || exception.IsRequested == true)
                    {
                        exception.AppointmentType += " отменена";
                        exception.IsCanceled = true;
                        exception.ReasonFromCanceled = model.ReasonFromCanceled;
                        this.weRepository.Update(exception);
                        var userRezerved = await this.userRepository.All().Where(x => x.Id == exception.UserId).Select(x => x.IsReserved).FirstOrDefaultAsync();
                        if (userRezerved)
                        {
                            await CancelApointmentЕmail(lawyerNames, exception.StarFrom, model.ReasonFromCanceled, lawyerId, exception.Email);
                        }
                    }
                    else
                    {
                        this.weRepository.HardDelete(exception);
                    }
                }
                await this.weRepository.SaveChangesAsync();
            }
        }

        public async Task CancelAppointmentInRangeAsync(CancelAppointmentInputModel model, string lawyerId)
        {
            var wtExceptions = await this.companyRepository.All().Where(x => x.Id == lawyerId).Select(x => x.WorkingTime).SelectMany(x => x.WorkingTimeExceptions).Where(x => x.Date >= model.FirstDate && x.Date <= model.LastDate).ToListAsync();
            if (wtExceptions == null) return;

            var lawyerNames = await this.companyRepository.All().Where(x => x.Id == lawyerId).Select(x => x.Names).FirstOrDefaultAsync();
            if (wtExceptions.Any())
            {

                foreach (var exception in wtExceptions)
                {
                    if (exception.AppointmentType.ToLower() == GlobalConstants.Meeting.ToLower())
                    {
                        continue;
                    }
                    if (exception.IsApproved == true || exception.IsRequested == true)
                    {
                        exception.AppointmentType += " отменена";
                        exception.IsCanceled = true;
                        exception.ReasonFromCanceled = model.ReasonFromCanceled;
                        this.weRepository.Update(exception);
                        var userRezerved = await this.userRepository.All().Where(x => x.Id == exception.UserId).Select(x => x.IsReserved).FirstOrDefaultAsync();
                        if (userRezerved)
                        {
                            await CancelApointmentЕmail(lawyerNames, exception.StarFrom, model.ReasonFromCanceled, lawyerId, exception.Email);
                        }
                    }
                    else
                    {
                        this.weRepository.HardDelete(exception);
                    }
                }
                await this.weRepository.SaveChangesAsync();
            }
        }

        public async Task<bool> FreeRequestByWteIdAsync(string wteId)
        {

            var wte = await this.weRepository.All().FirstOrDefaultAsync(x => x.Id == wteId);
            if (wte == null)
            {
                return false;
            }
            if (wte.IsRequested == true) return false;

            return true;
        }

        public async Task<IEnumerable<WorkingTimeExceptionMeetingViewModel>> GetMeetingWorkingTimeException(string lawyerId)
        {
            var exceptions = new List<WorkingTimeExceptionMeetingViewModel>();

            var results = await this.companyRepository.All().Where(x => x.Id == lawyerId).Select(x => x.WorkingTime.WorkingTimeExceptions).FirstOrDefaultAsync();
            var result = results.Where(x => x.AppointmentType == GlobalConstants.Meeting).ToList();
            if (result != null)
            {
                foreach (var item in result)
                {
                    var newExc = new WorkingTimeExceptionMeetingViewModel();
                    newExc.Id = item.Id;
                    newExc.MoreInformation = item.MoreInformation;
                    newExc.CaseNumber = item.CaseNumber;
                    newExc.Court = item.Court;
                    newExc.StarFrom = item.StarFrom;
                    newExc.TypeOfCase = item.TypeOfCase;
                    newExc.SideCase = item.SideCase;
                    exceptions.Add(newExc);
                }
            }

            return exceptions;
        }

        public async Task<IEnumerable<WorkingTimeExceptionBookingModel>> GetEarliestWteAsync(string lawyerId)
        {

            var wteId = await this.companyRepository.All().Where(x => x.Id == lawyerId).Select(x => x.WorkingTimeId).FirstAsync();
            var exception = await this.weRepository.All()
                .Where(x => x.WorkingTimeId == wteId)
                .Where(x => x.IsRequested == false && x.AppointmentType == GlobalConstants.Client && x.StarFrom > DateTime.Now).To<WorkingTimeExceptionBookingModel>().ToListAsync();
            return exception;
        }

        public async Task<int> GetAppointmentsCountAsync()
        {
            var count = await this.weRepository.All().Where(x => x.AppointmentType == GlobalConstants.Client && x.IsApproved == true).CountAsync();

            return count;
        }
        private async Task SendEmailToUser(string names, DateTime startFrom, string userEmail)
        {
            var messageBody = new StringBuilder();
            messageBody.AppendLine($"Благодарим ви, че резервирахте час при А-т {names}. Срещата ви е насрочена за {startFrom}.");
            messageBody.AppendLine($"Можете да видите подробности от <a href=\"{GlobalConstants.SendEmailToUserUrl}\"> тук</a>");

            await emailSender.SendEmailAsync(GlobalConstants.PlatformEmail, "PravenPortal", userEmail,
                "Благодарим ви, че резервирахте час",
                messageBody.ToString()
                );
        }
        private async Task SendEmailToLawyer(string firstName, string lastName, DateTime startFrom, string companyEmail)
        {
            var messageLawyerBody = new StringBuilder();
            messageLawyerBody.AppendLine($"Имате запазен час за среща от {firstName} {lastName}. Срещата ви е насрочена за {startFrom}.");
            messageLawyerBody.AppendLine($"Можете да видите подробности от <a href=\"{GlobalConstants.SendEmailToLawyerUrl}\"> тук</a>");

            await emailSender.SendEmailAsync(GlobalConstants.PlatformEmail, "Praven", companyEmail,
                "Имате запазен час за консултация",
                messageLawyerBody.ToString()
                );
        }
        private async Task CancelApointmentЕmail(string lawyerNames, DateTime startFrom, string reason, string lawyerId, string email)
        {
            var messageBody = new StringBuilder();
            messageBody.AppendLine($"Адвокат {lawyerNames} отмени срещата с вас насрочена за {startFrom}. Причината за отмяната е: {reason}");
            messageBody.AppendLine($"Можете да запазите нова среща от <a href=\"https://localhost:7245/lawyer/{lawyerId}\"> тук</a>");

            await emailSender.SendEmailAsync(GlobalConstants.PlatformEmail, "PravenPortal", email,
                "Отменена среща",
                messageBody.ToString()
                );
        }
    }
}