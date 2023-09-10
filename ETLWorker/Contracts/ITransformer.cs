namespace ETLWorker.Contracts
{
    
    public interface ITransformer<TInput, TOutput>
    {
        List<TOutput> TransformData(List<TInput> rawData);
    }


}
