using ETLWorker.Contracts;
using ETLWorker.DTO;

namespace ETLWorker.Implementations
{
    // Concrete ETL Orchestrator for transforming RawDataItem to Applicant
    public class LegacyToNewApplicateETLOrchestrator : ETLOrchestrator<LegacyApplicant, Applicant>
    {
        public LegacyToNewApplicateETLOrchestrator(IExtractor<LegacyApplicant> extractor, ITransformer<LegacyApplicant, Applicant> transformer, ILoader<Applicant> loader)
            : base(extractor, transformer, loader)
        {
        }

        public override void RunETLProcess()
        {
            Console.WriteLine("Starting RawDataItem to Applicant ETL Process...");
            ExecuteETLProcess();
            Console.WriteLine("RawDataItem to Applicant ETL Process completed.");
        }
    }


}
