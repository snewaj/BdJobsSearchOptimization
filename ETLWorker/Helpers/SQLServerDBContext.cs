using ETLWorker.DTO;
using Microsoft.EntityFrameworkCore;
namespace ETLWorker.Helpers
{
    public class SQLServerDBContext : DbContext
    {
        public DbSet<LegacyApplicant> LegacyApplicants { get; set; }

        public SQLServerDBContext(DbContextOptions<SQLServerDBContext> options) : base(options)
        {
        }
    }


}
