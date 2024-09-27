using Application.Services; 
using Application.ViewModels;
using Database.Contexts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MiniNetflix.Controllers
{
    public class DetailController : Controller
    {
        private readonly SerieService _serieService;

        public DetailController(ApplicationContext dbContext)
        {
            _serieService = new SerieService(dbContext);
        }


        public async Task<IActionResult> Index(int id) 
        {
            var serie = await _serieService.GetSerieByIdAsync(id); 
            if (serie == null) 
            {
                return NotFound();
            }

            return View(serie); 
        }
    }
}
