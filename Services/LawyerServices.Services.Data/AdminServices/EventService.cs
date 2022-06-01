using LawyerServices.Common;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using LawyerServices.Services.Messaging;
using Microsoft.EntityFrameworkCore;

namespace LawyerServices.Services.Data.AdminServices
{
    public class EventService : IEventService
    {
        private readonly IEmailSender emailSender;
        private readonly IDeletableEntityRepository<WorkingTimeException> weRepository;

        public EventService(IEmailSender emailSender, IDeletableEntityRepository<WorkingTimeException> weRepository)
        {
            this.emailSender = emailSender;
            this.weRepository = weRepository;
        }

        public async Task SendEventsEmailToLawyersAsync()
        { 
        
        }
        public async Task SendEventsEmailToLawyersUsersAsync()
        {
            var result = await this.weRepository.All()
                .Where(
                x=>x.Date.Date.AddDays(1) == DateTime.Now.Date.AddDays(1) 
                && x.AppointmentType == GlobalConstants.Client
                && x.IsRequested == true
                
                ).ToListAsync();
        }
        public async Task SendEventsEmailToNotaryUsersAsync()
        {
            
        }
        public async Task DeleteAllWteWhenDateIsOver()
        {
            var result = await this.weRepository.All()
                .Where(x => x.StarFrom < DateTime.Now && x.IsRequested == false && x.AppointmentType ==GlobalConstants.Client).ToListAsync();
            foreach (var wte in result)
            {
                this.weRepository.HardDelete(wte);
                await this.weRepository.SaveChangesAsync();
            }
        }
    }
}
