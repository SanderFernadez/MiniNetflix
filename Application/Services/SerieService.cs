using Application.Repository;
using Application.ViewModels;
using Database.Contexts;
using Microsoft.EntityFrameworkCore;
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
                    PortadaUrl = serie.PortadaUrl,




            }).ToList();
        }
        public async Task<List<SerieViewModel>> GetAllSeriesWithGenresAsync()
        {
            var seriesWithGenres = await _repository.Series
                .Include(s => s.Genre) // Asegúrate de incluir el género
                .Select(s => new SerieViewModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    PortadaUrl = s.PortadaUrl,
                    GenreId = s.GenreId,
                    GenreName = s.Genre.Name, // Asigna el nombre del género
                    ProducerId = s.ProducerId
                })
                .ToListAsync();

            return seriesWithGenres;
        }



    }
}
