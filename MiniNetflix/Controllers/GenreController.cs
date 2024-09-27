using Application.Services;
using Application.ViewModels;
using Database.Contexts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MiniNetflix.Controllers
{
    public class GenreController : Controller
    {
        private readonly GenreService _genreService;

       
        public GenreController(ApplicationContext dbContext)
        {
            _genreService = new(dbContext);
        }

       
        public async Task<IActionResult> Index()
        {
         
            var genres = await _genreService.GetAllGenresAsync();

            
            return View(genres);
        }


        public  IActionResult Create ()
        {

            return View("SaveGenre", new SaveGenreViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> Create(SaveGenreViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveGenre", vm);
            }

            await _genreService.AddGenreAsync(vm);
            return RedirectToRoute(new { controller = "Genre", action = "Index" });

        }



        public async Task<IActionResult>  Edit(int id)
        {

            return View("SaveGenre", await _genreService.GetGenreByIdAsync(id));
        }


        [HttpPost]
        public async Task<IActionResult> Edit(SaveGenreViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                return View("SaveGenre", vm);
            }

            await _genreService.UpdateGenreByAsync(vm);
            return RedirectToRoute(new { controller = "Genre", action = "Index" });

        }


        public async Task<IActionResult> Delete(int id)
        {
            var viewModel = await _genreService.GetGenreByIdAsync(id);
            if (viewModel == null)
            {
                return NotFound(); 
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> DeletePost(int id)
        {

            await _genreService.DeleteGenreAsync(id);
            return RedirectToRoute(new { Controller = "Genre", Action = "Index" });

        }




    }
}
