namespace LawyerServices.Services.Data.AdminServices
{
    public interface IEventService
    {

        public Task SendEventsEmailToLawyersUsersAsync();

        public Task SendEventsEmailToNotaryUsersAsync();

        public Task DeleteAllWteWhenDateIsOver();
        
    }
}
