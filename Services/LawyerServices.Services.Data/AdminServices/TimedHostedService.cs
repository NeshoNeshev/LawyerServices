using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LawyerServices.Services.Data.AdminServices
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private int executionCount = 0;

        private readonly IServiceScopeFactory scopeFactory;
        private Timer _timer;

        public TimedHostedService(IServiceScopeFactory scopeFactory)
        {

            this.scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {


            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromHours(24));
           
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            //todo iject service to work
            var count = Interlocked.Increment(ref executionCount);
            using (var scope = scopeFactory.CreateScope())
            {
                var events = scope.ServiceProvider.GetRequiredService<IEventService>();
                await events.DeleteAllWteWhenDateIsOver();
                await events.SendEventsEmailToLawyersUsersAsync();
                await events.SendEventsEmailToNotaryUsersAsync();
            }
            Console.WriteLine(count);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
