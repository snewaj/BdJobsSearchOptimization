using ETLWorker.Contracts;
using MongoDB.Driver;
namespace ETLWorker.Repositories
{
    // Repository implementation for MongoDB
    public class MongoRepository<T> : IGenericRepository<T>
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<T>(typeof(T).ToString());
        }

        public void Add(T item)
        {
            _collection.InsertOne(item);
        }
    }


}
