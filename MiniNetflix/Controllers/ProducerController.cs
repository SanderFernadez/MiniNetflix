using Application.Services;
using Application.ViewModels;
using Database.Contexts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MiniNetflix.Controllers
{
    public class ProducerController : Controller
    {
        private readonly ProducerService _producerService;

        public ProducerController(ApplicationContext dbContext)
        {
            _producerService = new(dbContext);
        }

       
        public async Task<IActionResult> Index()
        {
            var producers = await _producerService.GetAllProducersAsync();
            return View(producers);
        }

        
        public IActionResult Create()
        {
            return View("SaveProducer", new SaveProducerViewModel());
        }

      
        [HttpPost]
        public async Task<IActionResult> Create(SaveProducerViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveProducer", vm);
            }

            await _producerService.AddProducerAsync(vm);
            return RedirectToRoute(new { controller = "Producer", action = "Index" });
        }

       
        public async Task<IActionResult> Edit(int id)
        {
            return View("SaveProducer", await _producerService.GetProducerByIdAsync(id));
        }

    
        [HttpPost]
        public async Task<IActionResult> Edit(SaveProducerViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveProducer", vm);
            }

            await _producerService.UpdateProducerAsync(vm);
            return RedirectToRoute(new { controller = "Producer", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var viewModel = await _producerService.GetProducerByIdAsync(id);
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

       
        [HttpPost]
        public async Task<ActionResult> DeletePost(int id)
        {
            await _producerService.DeleteProducerAsync(id);
            return RedirectToRoute(new { controller = "Producer", action = "Index" });
        }
    }
}
