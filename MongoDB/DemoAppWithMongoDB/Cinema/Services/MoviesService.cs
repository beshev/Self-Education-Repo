namespace Cinema.Services
{
    using Cinema.Data;
    using Cinema.Models;
    using MongoDB.Driver;

    public class MoviesService : IMoviesService
    {
        private const string CollectionName = "Movies";
        private readonly IMongoConnection _mongoCinemaDB;

        public MoviesService(IMongoConnection mongo)
        {
            _mongoCinemaDB = mongo;
            _mongoCinemaDB.CollectioName = CollectionName;
        }

        public async Task CreateMovieAsync(string title, string description, DateTime releaseDate, TimeSpan duration)
        {
            var movie = new MovieModel
            {
                Title = title,
                Description = description,
                RealeaseDate = releaseDate,
                Duration = duration,
            };

            //var shorMovie = new ShorMovieModel
            //{
            //    Title = title,
            //    Genre = "Test",
            //};

            await _mongoCinemaDB.CreateRecordAsync(movie);
        }

        public async Task DeleteMovieAsync(string movieId)
        {
            await _mongoCinemaDB.DeleteRecordAsync<MovieModel>(movieId);
        }

        public async Task EditMovieAsync(string id, string title, string description, DateTime releaseDate, TimeSpan duration)
        {
            var movie = new MovieModel
            {
                Id = id,
                Title = title,
                Description = description,
                RealeaseDate = releaseDate,
                Duration = duration,
            };

            await _mongoCinemaDB.UpdateRecordAsync(id, movie);
        }

        public IEnumerable<MovieModel> GetAllMovies()
        {
            var collection  = _mongoCinemaDB.GetCollection<MovieModel>()
                .AsQueryable()
                .OrderByDescending(x => x.RealeaseDate)
                .ThenByDescending(x => x.Duration);

            return collection.ToList();
        }

        public T GetById<T>(string id)
        {
            var collection = _mongoCinemaDB.GetCollection<T>();
            var filter = Builders<T>.Filter.Eq("Id", id);

            return collection.Find(filter).FirstOrDefault();
        }
    }
}
