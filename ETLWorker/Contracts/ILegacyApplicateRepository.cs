namespace ETLWorker.Contracts
{
    public interface ILegacyApplicateRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
    }


}
