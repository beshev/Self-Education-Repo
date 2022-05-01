namespace Cinema.Controllers
{
    using Cinema.Models;
    using Cinema.Services;
    using Microsoft.AspNetCore.Mvc;

    public class MoviesController : Controller
    {
        private readonly IMoviesService _moviesService;

        public MoviesController(IMoviesService moviesService)
        {
            this._moviesService = moviesService;
        }

        public IActionResult All()
        {
            var viewModel = _moviesService.GetAllMovies();

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieModel model)
        {
            await _moviesService.CreateMovieAsync(
                model.Title,
                model.Description,
                model.RealeaseDate,
                model.Duration);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Edit(string id)
        {
            var viewModel = _moviesService.GetById<MovieModel>(id);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MovieModel model)
        {
            await _moviesService.EditMovieAsync(
                model.Id,
                model.Title,
                model.Description,
                model.RealeaseDate,
                model.Duration);

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _moviesService.DeleteMovieAsync(id);

            return RedirectToAction(nameof(All));
        }
    }
}
