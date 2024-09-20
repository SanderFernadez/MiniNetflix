using Application.Repository;
using Application.Services;
using Database.Contexts;
using Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            var series = await _SerieService.GetAllSeriesWithGenresAsync();

            return View(await _SerieService.GetAllSeriesWithGenresAsync());
        }

      

      
    }
}
