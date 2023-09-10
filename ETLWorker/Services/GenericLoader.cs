using ETLWorker.Contracts;

namespace ETLWorker.Services
{
    public class GenericLoader<T> : ILoader<T>
    {
        private readonly IGenericRepository<T> _repository;

        public GenericLoader(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public void LoadData(List<T> data)
        {
            foreach (var item in data)
            {
                _repository.Add(item);
            }
        }
    }


}
