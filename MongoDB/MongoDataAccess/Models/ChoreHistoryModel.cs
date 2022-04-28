namespace MongoDataAccess.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class ChoreHistoryModel
    {
        public ChoreHistoryModel()
        {

        }

        public ChoreHistoryModel(ChoreModel chore)
        {
            ChoreId = chore.Id;
            ChoreText = chore.ChoreText;
            DateCompleted = chore.LastCompleted ?? DateTime.UtcNow;
            WhoCompleted = chore.AssignedTo;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string ChoreId { get; set; }

        public string ChoreText { get; set; }

        public DateTime DateCompleted { get; set; }

        public UserModel WhoCompleted { get; set; }
    }
}
