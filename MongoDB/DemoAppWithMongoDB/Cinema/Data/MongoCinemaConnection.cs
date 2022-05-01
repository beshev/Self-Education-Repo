namespace Cinema.Data
{
    using MongoDB.Driver;

    public class MongoCinemaConnection : IMongoConnection
    {
        private string collectionName;
        private readonly IMongoDatabase _database;


        public MongoCinemaConnection(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DefaultConnection"]);
            _database = client.GetDatabase(configuration["DatabaseName"]);
        }

        public string CollectioName
        {
            get => collectionName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("The collection name should be a valid string.");
                }

                collectionName = value;
            }
        }

        public async Task CreateRecordAsync<T>(T record)
        {
            await _database.GetCollection<T>(CollectioName).InsertOneAsync(record);
        }

        public async Task DeleteFromRecordAsync<T>(string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);

            await _database
                .GetCollection<T>(CollectioName)
                .DeleteOneAsync(filter);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return _database
                .GetCollection<T>(CollectioName);
        }

        public async Task UpdateRecordAsync<T>(string id, T record)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);

            await _database
                .GetCollection<T>(CollectioName)
                .ReplaceOneAsync(filter, record, new ReplaceOptions { IsUpsert = true });
        }
    }
}
