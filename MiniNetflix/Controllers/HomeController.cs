using Application.Services;
using Application.ViewModels;
using Database.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MiniNetflix.Controllers
{
    public class HomeController : Controller
    {
        private readonly SerieService _serieService;

        public HomeController(ApplicationContext dbContext)
        {
            _serieService = new SerieService(dbContext);
        }

        public async Task<IActionResult> Index(int? genreId, int? producerId, string searchQuery)
        {
            ViewData["CurrentController"] = "Home";

            
            var genres = await _serieService.GetAllGenresAsync();
            var producers = await _serieService.GetAllProducersAsync();

         
            if (genres == null || producers == null)
            {
                return View("Error"); 
            }

            
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

            List<SerieViewModel> series;

           
            if (!string.IsNullOrEmpty(searchQuery))
            {
                series = await _serieService.SearchSeriesAsync(searchQuery);
            }
          
            else if (genreId.HasValue || producerId.HasValue)
            {
                series = await _serieService.FilterSeriesAsync(genreId, producerId);
            }
            else
            {
                series = await _serieService.GetAllSeriesWithGenresAndProducerAsync();
            }

            return View(series);
        }


    }
}
