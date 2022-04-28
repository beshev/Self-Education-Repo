using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBDemo
{
    [BsonIgnoreExtraElements]
    public record Record(string Title, string Description);
}
