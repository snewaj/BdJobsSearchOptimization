namespace ETLWorker.Contracts
{
    public interface ILoader<T>
    {
        void LoadData(List<T> transformedData);
    }


}
