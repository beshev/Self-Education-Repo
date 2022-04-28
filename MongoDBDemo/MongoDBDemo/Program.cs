using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace MongoDBDemo
{
    internal class Program
    {
        static void Main()
        {
            string datebase = "Cinema";
            string collectionName = "Movies";

            var mongoDB = new MongoDb(datebase);

            var movie = new Movie
            {
                Title = "Friday",
                Description = "It's friday movie ;)",
                RealeaseDate = new DateTime(2000, 4, 25, 0, 0, 0, DateTimeKind.Utc),
                Duration = new TimeSpan(1, 44, 32),
                Halls = new List<MovieHall>
                 {
                     new MovieHall
                     {
                         Name = "Hall one",
                         FreeSits =  20,
                         ProjectionDate = new DateTime(2022,05,2, 18, 30, 00, DateTimeKind.Utc),
                     },
                     new MovieHall
                     {
                         Name = "VIP Hall",
                         FreeSits =  2,
                         ProjectionDate = new DateTime(2022,05,1, 18, 30, 00, DateTimeKind.Utc),
                     }
                 }
            };

            // mongoDB.InsertRecord(collectionName, movie);

            var moviesCollection = mongoDB.LoadRecords<Movie>(collectionName);
            Console.WriteLine(string.Join(Environment.NewLine, moviesCollection));

            // var recordTest = mongoDB.LoadRecords<Record>(collectionName);
            // Console.WriteLine(string.Join(Environment.NewLine, recordTest));
        }
    }

    class MongoDb
    {
        private IMongoDatabase _db;

        public MongoDb(string datebase)
        {
            var client = new MongoClient();

            _db = client.GetDatabase(datebase);
        }

        public void InsertRecord<T>(string table, T reconrd)
        {
            var collection = _db.GetCollection<T>(table);
            collection.InsertOne(reconrd);
        }

        public List<T> LoadRecords<T>(string table)
        {
            var collection = _db.GetCollection<T>(table);

            return collection.Find(new BsonDocument()).ToList();
        }

        public T LoadRecordById<T>(string table, string id)
        {
            var collection = _db.GetCollection<T>(table);
            var filer = Builders<T>.Filter.Eq("Id", id);

            return collection.Find(filer).First();
        }

        public void UpsertRecord<T>(string table, string id, T record)
        {
            var collection = _db.GetCollection<T>(table);

            collection
                .ReplaceOne(
                new BsonDocument("_id", new ObjectId(id)),
                record,
                new ReplaceOptions() { IsUpsert = false });
        }

        public void DeleteRecord<T>(string table, string id)
        {
            var collection = _db.GetCollection<T>(table);
            var filer = Builders<T>.Filter.Eq("Id", id);

            collection.DeleteOne(filer);
        }
    }
}
