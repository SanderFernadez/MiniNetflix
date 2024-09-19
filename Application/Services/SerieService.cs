using Application.Repository;
using Application.ViewModels;
using Database.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SerieService
    {
        private readonly SerieRepository _repository;

        public SerieService( ApplicationContext dbContext)
        {
            _repository = new(dbContext);
        }


        public async Task<List<SerieViewModel>>GetAllViewModel()
        {
            var SerieList = await _repository.GetAllAsync();
            return SerieList.Select(serie => new SerieViewModel
            {
                    Title = serie.Title,
                    Description = serie.Description,
                    Id = serie.Id,
                    VideoUrl = serie.VideoUrl,
                    PortadaUrl = serie.PortadaUrl



            }).ToList();
        }




    }
}
