using ETLWorker.Contracts;
using ETLWorker.DTO;
using ETLWorker.Helpers;
using ETLWorker.Services;

namespace ETLWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    var serviceProvider = new ServiceCollection()
                .AddScoped<ITransformer<LegacyApplicant, Applicant>, LegacyApplicateToNewApplicantTransformer>()
                .AddScoped<ILoader<Applicant>, GenericLoader<Applicant>>()
                .AddScoped<ETLOrchestrator<LegacyApplicant, Applicant>, LegacyToNewApplicateETLOrchestrator>()
                .BuildServiceProvider();
                ServiceFactory serviceFactory = new ServiceFactory(serviceProvider);
                services.AddHostedService<Worker>();
                })
                .Build();

            host.Run();
        }
    }
}