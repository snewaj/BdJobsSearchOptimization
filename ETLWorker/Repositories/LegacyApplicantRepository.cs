using ETLWorker.Contracts;
using ETLWorker.DTO;
using ETLWorker.Helpers;
using MongoDB.Driver;
namespace ETLWorker.Repositories
{
    // Repository implementation
    public class LegacyApplicantRepository : ILegacyApplicateRepository<LegacyApplicant>
    {
        private readonly SQLServerDBContext _dbContext;

        public LegacyApplicantRepository(SQLServerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        IEnumerable<LegacyApplicant> ILegacyApplicateRepository<LegacyApplicant>.GetAll()
        {
            return _dbContext.LegacyApplicants.ToList();
        }
    }


}
