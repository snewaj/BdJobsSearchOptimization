using ETLWorker.Contracts;
using ETLWorker.DTO;
using ETLWorker.Helpers;

namespace ETLWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                // Resolve the ETL process manager
                var etlProcessManager = ServiceFactory.GetProvider().GetRequiredService<ETLOrchestrator<LegacyApplicant, Applicant>>();

                // Execute the ETL process
                etlProcessManager.RunETLProcess();   
                await Task.Delay(1000);
            }
        }
    }
}