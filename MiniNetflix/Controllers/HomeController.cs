using Application.Services;
using Database.Contexts;
using Microsoft.AspNetCore.Mvc;
using MiniNetflix.Models;
using System.Diagnostics;

namespace MiniNetflix.Controllers
{
    public class HomeController : Controller
    {
        private readonly SerieService _SerieService;

        public HomeController(ApplicationContext dbContext)
        {
            _SerieService = new (dbContext);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _SerieService.GetAllViewModel());
        }

      

      
    }
}
