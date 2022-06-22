using LaweyrServices.Web.Shared.WorkingTimeModels;
using LawyerServices.Common;
using LawyerServices.Data.Models;
using LawyerServices.Data.Models.Enumerations;
using LawyerServices.Data.Repositories;
using LawyerServices.Services.Messaging;
using Microsoft.EntityFrameworkCore;
using System.Text;
using LawyerServices.Services.Mapping;

namespace LawyerServices.Services.Data.AdminServices
{
    public class EventService : IEventService
    {
        private readonly IEmailSender emailSender;
        private readonly IDeletableEntityRepository<WorkingTimeException> weRepository;
        private readonly IDeletableEntityRepository<Company> companyRepository;
        private readonly ITimeService timeService;
        public EventService(IEmailSender emailSender, IDeletableEntityRepository<WorkingTimeException> weRepository, IDeletableEntityRepository<Company> companyRepository, ITimeService timeService)
        {
            this.emailSender = emailSender;
            this.weRepository = weRepository;
            this.companyRepository = companyRepository;
            this.timeService = timeService;
        }

        public async Task SendEventsEmailToLawyersUsersAsync()
        {
            var date = this.timeService.GetTimeOffset(DateTime.Now);
           
            var wteExceptions = await this.weRepository.All()
                .Where(w => w.User.IsReminderForComing == true && w.StarFrom.Date == date.AddDays(1).Date && w.IsCanceled == false && w.IsRequested == true)
                .To<WorkingTimeExceptionEmailModel>().ToListAsync();

            if (wteExceptions.Any())
            {
                foreach (var exception in wteExceptions)
                {
                    var messageBody = new StringBuilder();
                    messageBody.AppendLine($"Здравейте {exception.FirstName} {exception.LastName}");
                    messageBody.AppendLine($"Напомняме Ви, че имате насрочена среща с Адвокат {exception.WorkingTime.Companies.First().Names} насрочена за { exception.StarFrom.ToString("MM/dd/yyyy HH:mm")}");
                    messageBody.AppendLine($"Балгодарим Ви, че използвате PravenPortal");

                    await emailSender.SendEmailAsync(GlobalConstants.PlatformEmail, "PravenPortal", exception.Email,
                        "Напомняне за сереща !",
                        messageBody.ToString()
                        );
                }
            }
        }
        public async Task SendEventsEmailToNotaryUsersAsync()
        {
            var date = this.timeService.GetTimeOffset(DateTime.Now);
            var exceptions = await this.companyRepository.All().Where(x => x.Profession == (Profession)Enum.Parse(typeof(Profession), "Notary"))
                .Where(x=>x.IsReminderForComing == true)
                .Select(x=>x.WorkingTime).SelectMany(x=>x.WorkingTimeExceptions)
                .Where(x=> x.Date.Date == date.AddDays(1).Date).ToListAsync();
            if (exceptions.Any())
            {
                foreach (var exception in exceptions)
                {
                    var company = await this.companyRepository.All().Where(x => x.WorkingTimeId == exception.WorkingTimeId).FirstOrDefaultAsync();
                    var messageBody = new StringBuilder();
                    messageBody.AppendLine($"Напомняме Ви, че имате насрочена среща с Нотариус {company.Names}, насрочена за { exception.StarFrom.ToString("MM/dd/yyyy HH:mm")}");
                    messageBody.AppendLine($"Балгодарим Ви, че използвате PravenPortal");

                    await emailSender.SendEmailAsync(GlobalConstants.PlatformEmail, "PravenPortal", exception.Email,
                        "Напомняне за сереща !",
                        messageBody.ToString()
                        );
                }
            }
            
        }
        public async Task DeleteAllWteWhenDateIsOver()
        {
            var date = this.timeService.GetTimeOffset(DateTime.Now);
            var result = await this.weRepository.All()
                .Where(x => x.StarFrom.Date < date.AddDays(-1).Date && x.IsRequested == false && x.AppointmentType == GlobalConstants.Client).ToListAsync();
            foreach (var wte in result)
            {
                this.weRepository.HardDelete(wte);
                await this.weRepository.SaveChangesAsync();
            }
        }
    }
}
