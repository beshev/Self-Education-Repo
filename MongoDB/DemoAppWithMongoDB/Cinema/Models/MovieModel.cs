namespace Cinema.Models
{
    public class MovieModel : BsonBaseModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime RealeaseDate { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
