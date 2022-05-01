namespace Cinema.Data
{
    using MongoDB.Driver;

    public interface IMongoConnection
    {
        public string CollectioName { get; set; }

        public IMongoCollection<T> GetCollection<T>();

        public Task CreateRecordAsync<T>(T record);

        public Task UpdateRecordAsync<T>(string id, T record);

        public Task DeleteFromRecordAsync<T>(string id);
    }
}
