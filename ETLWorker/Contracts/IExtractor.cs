namespace ETLWorker.Contracts
{
    public interface IExtractor<T>
    {
        List<T> ExtractData();
    }


}
