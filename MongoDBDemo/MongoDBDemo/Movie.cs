using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDBDemo
{
    public class Movie
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TimeSpan Duration { get; set; }

        public DateTime RealeaseDate { get; set; }

        public ICollection<MovieHall> Halls { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Title: {Title}");
            sb.AppendLine($"Description: {Description}");
            sb.AppendLine($"Realease date: {RealeaseDate.ToLongDateString()}");
            sb.AppendLine($"Duration: {Duration}");

            if (Halls != null)
            {
                sb.AppendLine(" Projection Halls");

                foreach (var hall in Halls)
                {
                    sb
                        .AppendLine($"  Hall name: {hall.Name}")
                        .AppendLine($"  Free spaces: {hall.FreeSits}")
                        .AppendLine($"  Projection date: {hall.ProjectionDate.ToLongDateString()} {hall.ProjectionDate.ToLongTimeString()}")
                        .AppendLine();
                }
            }

            return sb.ToString();
        }
    }

}
