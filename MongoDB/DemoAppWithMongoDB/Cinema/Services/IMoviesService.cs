namespace Cinema.Services
{
    using Cinema.Models;

    public interface IMoviesService
    {
        public T GetById<T>(string id);

        Task CreateMovieAsync(string title, string description, DateTime releaseDate, TimeSpan duration);

        Task DeleteMovieAsync(string movieId);

        IEnumerable<MovieModel> GetAllMovies();

        Task EditMovieAsync(string id, string title, string description, DateTime releaseDate, TimeSpan duration);
    }
}
