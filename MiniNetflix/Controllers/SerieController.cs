using Application.Services;
using Application.ViewModels;
using Database.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MiniNetflix.Controllers
{
    public class SerieController : Controller
    {
        private readonly SerieService _SerieService;

        public SerieController(ApplicationContext dbContext)
        {
            _SerieService = new(dbContext);
        }

        public async Task<IActionResult> Index(int? genreId, int? producerId, string searchQuery)
        {

            ViewData["CurrentController"] = "Serie";

            List<SerieViewModel> series;

            if (!string.IsNullOrEmpty(searchQuery))
            {
                series = await _SerieService.SearchSeriesAsync(searchQuery);
            }
            else if (genreId.HasValue || producerId.HasValue)
            {
                series = await _SerieService.FilterSeriesAsync(genreId, producerId);
            }
            else
            {
                series = await _SerieService.GetAllSeriesWithGenresAndProducerAsync();
            }

            // Cargar géneros y productores
            var genres = await _SerieService.GetAllGenresAsync();
            ViewBag.Genres = genres.Select(g => new
            {
                Value = g.Id,
                Text = g.Name
            }).ToList();

            var producers = await _SerieService.GetAllProducersAsync();
            ViewBag.Producers = producers.Select(p => new
            {
                Value = p.Id,
                Text = p.Name
            }).ToList();

            return View(series);
        }


        public async Task<IActionResult> Create()
        {
            var genres = await _SerieService.GetAllGenresAsync();
            var producers = await _SerieService.GetAllProducersAsync();

            ViewBag.Genres = genres.Select(g => new
            {
                Value = g.Id,
                Text = g.Name
            }).ToList();

            ViewBag.Producers = producers.Select(p => new
            {
                Value = p.Id,
                Text = p.Name
            }).ToList();

            ViewBag.SecondaryGenres = await _SerieService.GetAllGenresAsync();

            return View("SaveSerie", new SaveSerieViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Create(SaveSerieViewModel vm)
        {

           

            if (ModelState.IsValid)
            {
                await _SerieService.Add(vm);
                return RedirectToAction("Index", "Serie");
            }


            // Cargar géneros y productores nuevamente en caso de que haya un error
            await LoadViewBagData();
            return View("SaveSerie", vm);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await _SerieService.GetByIdForEditAsync(id);
            if (viewModel == null)
            {
                return NotFound(); // Manejar el caso nulo
            }

           

            // Cargar géneros y productores
            await LoadViewBagData();
            return View("SaveSerie", viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(SaveSerieViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _SerieService.Update(vm);
                return RedirectToRoute(new { Controller = "Serie", Action = "Index" });
            }

           

            // Cargar géneros y productores nuevamente en caso de que haya un error
            await LoadViewBagData();
            return View("SaveSerie", vm);
        }





        public async Task<IActionResult> Delete(int id)
        {
            var viewModel = await _SerieService.GetByIdForEditAsync(id);
            if (viewModel == null)
            {
                return NotFound(); 
            }



          
            await LoadViewBagData();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> DeletePost(int id)
        {
         
           
             await _SerieService.Delete(id);
             await LoadViewBagData();
             return RedirectToRoute(new { Controller = "Serie", Action = "Index" });
           
        }


        private async Task LoadViewBagData()
        {
            var genres = await _SerieService.GetAllGenresAsync();
            ViewBag.Genres = genres.Select(g => new
            {
                Value = g.Id,
                Text = g.Name
            }).ToList();

            var producers = await _SerieService.GetAllProducersAsync();
            ViewBag.Producers = producers.Select(p => new
            {
                Value = p.Id,
                Text = p.Name
            }).ToList();

            ViewBag.SecondaryGenres = await _SerieService.GetAllGenresAsync();
        }
    }
}
