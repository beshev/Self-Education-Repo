namespace MongoDataAccess.DataAccess
{
    using MongoDataAccess.Models;
    using MongoDB.Driver;

    public class ChoreDataAccess
    {
        // These constants are here for the demo. Usually, they should be in the appsettings.json file.
        private const string ConnectionString = "Your mongo connection string";
        private const string DatabaseName = "choredb";
        private const string ChoreCollection = "chore_chart";
        private const string UserCollection = "users";
        private const string ChoreHistoryCollection = "chore_hostory";

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            var usersCollection = ConnectToMongo<UserModel>(UserCollection);
            var result = await usersCollection.FindAsync(_ => true);
            return result.ToList();
        }

        public Task CreateUserAsync(UserModel user)
        {
            var usersCollection = ConnectToMongo<UserModel>(UserCollection);
            return usersCollection.InsertOneAsync(user);
        }

        public async Task<List<ChoreModel>> GetAllChoresAsync()
        {
            var choresCollection = ConnectToMongo<ChoreModel>(ChoreCollection);
            var result = await choresCollection.FindAsync(_ => true);
            return result.ToList();
        }

        public Task CreateChoreAsync(ChoreModel chore)
        {
            var choreCollection = ConnectToMongo<ChoreModel>(ChoreCollection);
            return choreCollection.InsertOneAsync(chore);
        }

        public Task UpdateChoreAsync(ChoreModel chore)
        {
            var choreCollection = ConnectToMongo<ChoreModel>(ChoreCollection);
            var filter = Builders<ChoreModel>.Filter.Eq("Id", chore.Id);

            return choreCollection.ReplaceOneAsync(filter, chore, new ReplaceOptions { IsUpsert = true});
        }

        public Task DeleteChoreAsync(ChoreModel chore)
        {
            var choreCollection = ConnectToMongo<ChoreModel>(ChoreCollection);
            return choreCollection.DeleteOneAsync(x => x.Id == chore.Id);
        }

        public async Task CompleteChore(ChoreModel chore)
        {
            var client = new MongoClient(ConnectionString);
            using var session = await client.StartSessionAsync();

            // The transaction works only in Atlas, which is a mongo cloud service!!
            session.StartTransaction();

            try
            {
                var db = client.GetDatabase(DatabaseName);
                var choresCollection = db.GetCollection<ChoreModel>(ChoreCollection);
                var filter = Builders<ChoreModel>.Filter.Eq("Id", chore.Id);
                await choresCollection.ReplaceOneAsync(filter, chore);

                var choreHistoryCollection = db.GetCollection<ChoreHistoryModel>(ChoreHistoryCollection);
                await choreHistoryCollection.InsertOneAsync(new ChoreHistoryModel(chore));

                await session.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                session.AbortTransaction();
                Console.WriteLine($"An exception was thrown! With message: {ex.Message}");
            }

        }

        public async Task<List<ChoreModel>> GetAllChoresForAUserAsync(UserModel user)
        {
            var choresCollection = ConnectToMongo<ChoreModel>(ChoreCollection);
            var result = await choresCollection.FindAsync(x => x.AssignedTo != null && x.AssignedTo.Id == user.Id);
            return result.ToList();
        }

        private IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {
            var client = new MongoClient(ConnectionString);
            var db = client.GetDatabase(DatabaseName);
            return db.GetCollection<T>(collection);
        }
    }
}
