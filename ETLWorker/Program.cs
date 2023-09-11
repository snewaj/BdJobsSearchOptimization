using ETLWorker.Contracts;
using ETLWorker.DTO;
using ETLWorker.Helpers;
using ETLWorker.Implementations;
using ETLWorker.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ETLWorker
{
    //DbContextOptions
    public class Program
    {
        //IGenericRepository
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    var serviceProvider = new ServiceCollection()
                .AddSingleton<IGenericRepository<Applicant>, MongoRepository<Applicant>>()
                .AddScoped<ITransformer<LegacyApplicant, Applicant>, LegacyApplicateToNewApplicantTransformer>()
                .AddScoped<ILoader<Applicant>, GenericLoader<Applicant>>()
                .AddScoped<IExtractor<LegacyApplicant>, LegacyApplicantExtractor<LegacyApplicant>>()
                .AddScoped<ETLOrchestrator<LegacyApplicant, Applicant>, LegacyToNewApplicateETLOrchestrator>()
                .AddScoped<ILegacyApplicateRepository<LegacyApplicant>, LegacyApplicantRepository>()
                .AddScoped<SQLServerDBContext, SQLServerDBContext>()
                .AddScoped<DbContextOptions<SQLServerDBContext>, DbContextOptions<SQLServerDBContext>>()
                .BuildServiceProvider();
                ServiceFactory serviceFactory = new ServiceFactory(serviceProvider);
                services.AddHostedService<Worker>();
                })
                .Build();

            host.Run();
        }
    }
}