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
        private readonly IDeletableEntityRepository<Company> companyRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<WorkingTimeException> weRepository;
        private readonly IEmailSender emailSender;
        public WorkingTimeExceptionService(IDeletableEntityRepository<Company> companyRepository,
            IDeletableEntityRepository<WorkingTimeException> weRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository, IEmailSender emailSender
            )
        {
            this.companyRepository = companyRepository;
            this.weRepository = weRepository;
            this.userRepository = userRepository;
            this.emailSender = emailSender;
        }

        public async Task SendRequestToLawyerAsync(UserRequestModel? userRequestModel)
        {
            var user = await this.userRepository.All().Where(x => x.Id == userRequestModel.UserId).FirstOrDefaultAsync();
            var company = await this.companyRepository.All().FirstOrDefaultAsync(x => x.Id == userRequestModel.CompanyId);
            var userCompany = await this.userRepository.All().Where(x => x.CompanyId == userRequestModel.CompanyId).FirstOrDefaultAsync();
            var workingTimeException = await this.weRepository.All().FirstOrDefaultAsync(ex => ex.Id == userRequestModel.WorkingTimeExceptionId);
            if (workingTimeException == null) return;
            if (company == null) return;
            if (userCompany == null) return;
            if (user == null) return;
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
                   await  companyRepository.SaveChangesAsync();
                }
               
                this.weRepository.Update(workingTimeException);
                await this.weRepository.SaveChangesAsync();

                if (userRequestModel.IsReminderForComing)
                {
                    var messageBody = new StringBuilder();
                    messageBody.AppendLine($"Благодарим ви, че резервирахте час при А-т {company.Names}. Срещата ви е насрочена за {workingTimeException.StarFrom}.");
                    messageBody.AppendLine("Можете да видите подробности от <a href=\"https://localhost:7245/client\"> тук</a>");

                    await emailSender.SendEmailAsync("neshevgmail@abv.bg", "PravenPortal", userRequestModel.Email,
                        "Благодарим ви, че резервирахте час",
                        messageBody.ToString()
                        );
                }
                var messageLawyerBody = new StringBuilder();
                messageLawyerBody.AppendLine($"Имате запазен час за среща от {userRequestModel.FirstName} {userRequestModel.LastName}. Срещата ви е насрочена за {workingTimeException.StarFrom}.");
                messageLawyerBody.AppendLine("Можете да видите подробности от <a href=\"https://localhost:7245/client\"> тук</a>");

                await emailSender.SendEmailAsync("neshevgmail@abv.bg", "Praven", userCompany.Email,
                    "Благодарим ви, че резервирахте час",
                    messageLawyerBody.ToString()
                    );

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
            //var exceptions = this.userRepository.All().Where(u => u.Id == wteId).
            //    Select(c => c.Company)
            //    .Select(w => w.WorkingTime)
            //    .Select(wte => wte.WorkingTimeException.Where(x=>x.IsRequested == true)).FirstOrDefault(); 
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
            //var exceptions = this.userRepository.All().Where(u => u.Id == userId).
            //    Select(c => c.Company)
            //    .Select(w => w.WorkingTime)
            //    .Select(wte => wte.WorkingTimeException.Where(x => x.IsRequested == true)).To<WorkingTimeExceptionBookingModel>().ToList();

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

        public async Task SetIsApprovedAsync(string wteId)
        {
            var wte = this.weRepository.All().Where(x => x.Id == wteId).FirstOrDefault();
            if (wte != null)
            {
                wte.IsApproved = false;
                wte.IsCanceled = true;
                this.weRepository.Update(wte);
                await this.weRepository.SaveChangesAsync();
            }
        }

        public async Task<bool> SetNotSHowUpAsync(string wteId)
        {
            var wte = this.weRepository.All().FirstOrDefault(w => w.Id == wteId);

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
        public async Task SetWorkingTimeExceptionToFreeAsync(string wteId, string userId)
        {
            var wte = this.weRepository.All().FirstOrDefault(w => w.Id == wteId);

            var user = this.userRepository.All().FirstOrDefault(u => u.Id == userId);

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
            var wtExceptions = await this.companyRepository.All().Where(x => x.Id == lawyerId).Select(x => x.WorkingTime).SelectMany(x => x.WorkingTimeExceptions).ToListAsync();
            if (wtExceptions == null) return;
            var lawyer = await this.companyRepository.All().Where(x=>x.Id == model.LawyerId).Select(x=>x.Names).FirstOrDefaultAsync();

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
                        exception.IsCanceled = true;
                        exception.ReasonFromCanceled = model.ReasonFromCanceled;
                        this.weRepository.Update(exception);
                        var messageBody = new StringBuilder();
                        messageBody.AppendLine($"Адвокат {lawyer} отмени срещата с вас насрочена за {exception.StarFrom}. Причината за отмяната е: {model.ReasonFromCanceled}");
                        messageBody.AppendLine($"Можете да запазите нова среща от <a href=\"https://localhost:7245/lawyer/{model.LawyerId}\"> тук</a>");

                        await emailSender.SendEmailAsync("neshevgmail@abv.bg", "PravenPortal", exception.User.Email,
                            "Отменена среща",
                            messageBody.ToString()
                            );
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
            
            var lawyer = await this.companyRepository.All().Where(x => x.Id == model.LawyerId).Select(x => x.Names).FirstOrDefaultAsync();
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
                        exception.IsCanceled = true;
                        exception.ReasonFromCanceled = model.ReasonFromCanceled;
                        this.weRepository.Update(exception);
                        var messageBody = new StringBuilder();
                        messageBody.AppendLine($"Адвокат {wtExceptions} отмени срещата с вас насрочена за {exception.StarFrom}. Причината за отмяната е: {model.ReasonFromCanceled}");
                        messageBody.AppendLine($"Можете да запазите нова среща от <a href=\"https://localhost:7245/lawyer/{model.LawyerId}\"> тук</a>");

                        await emailSender.SendEmailAsync("neshevgmail@abv.bg", "PravenPortal", exception.User.Email,
                            "Отменена среща",
                            messageBody.ToString()
                            );
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
    }
}