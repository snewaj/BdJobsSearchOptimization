using ETLWorker.Contracts;
using ETLWorker.DTO;

namespace ETLWorker.Services
{
    // Implement Transformer and Loader with the specified data types
    public class LegacyApplicateToNewApplicantTransformer : ITransformer<LegacyApplicant, Applicant>
    {
        public List<Applicant> TransformData(List<LegacyApplicant> legacyApplicants)
        {
            
            List<Applicant> transformedData = new List<Applicant>();
            foreach (var item in legacyApplicants)
            {
                // Here, you should implement logic to convert LegectApplicant to Applicant
                var applicant = new Applicant { Name = item.LegacyName };
                transformedData.Add(applicant);
            }
            return transformedData;
        }
    }


}
