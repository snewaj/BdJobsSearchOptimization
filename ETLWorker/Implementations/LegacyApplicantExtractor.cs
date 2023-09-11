using ETLWorker.Contracts;

namespace ETLWorker.Implementations
{
    // Implement the extractor using the repository pattern
    public class LegacyApplicantExtractor<T> : IExtractor<T>
    {
        private readonly ILegacyApplicateRepository<T> _repository;

        public LegacyApplicantExtractor(ILegacyApplicateRepository<T> repository)
        {
            _repository = repository;
        }

        public List<T> ExtractData()
        {
            return (List<T>)_repository.GetAll();
        }
    }


}
