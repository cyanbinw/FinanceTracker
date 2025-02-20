
namespace FinanceTracker.Service.BackgroundServices
{
    public class WorkerService : BackgroundService
    {
        public WorkerService() 
        {
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (stoppingToken.IsCancellationRequested) 
            {
                await Task.Delay(1000);
            }
        }
    }
}
