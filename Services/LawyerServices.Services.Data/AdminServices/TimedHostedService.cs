using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LawyerServices.Services.Data.AdminServices
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private int executionCount = 0;

        private Timer _timer;

        public TimedHostedService()
        {
   
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
           

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            //todo iject service to work
            var count = Interlocked.Increment(ref executionCount);
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
