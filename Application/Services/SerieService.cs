using Application.Repository;
using Application.ViewModels;
using Database.Contexts;
using Database.Entities;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ApplicationContext _dbContext;

        public SerieService( ApplicationContext dbContext)
        {
            _repository = new(dbContext);
            _dbContext = dbContext;
        }


        public async Task Update(SaveSerieViewModel vm)
        {
            var serie = await _repository.GetByIdAsync(vm.Id);
            if (serie == null)
            {
                throw new Exception("Serie no encontrada");
            }

            
            serie.Title = vm.Title;
            serie.VideoUrl = vm.VideoUrl;
            serie.PortadaUrl = vm.PortadaUrl;
            serie.Description = vm.Description;
            serie.ProducerId = vm.ProducerId;
            serie.GenreId = vm.GenreId;
            serie.SecondaryGenresIds = vm.SecondaryGenresIds;

            await _repository.UpdateAsync(serie);

        
            await _repository.UpdateGenresAsync(serie);
        }



        public async Task Add(SaveSerieViewModel vm)
        {
            Serie serie = new()
            {
                Id = vm.Id,
                Title = vm.Title,
                Description = vm.Description,
                VideoUrl = vm.VideoUrl,
                PortadaUrl = vm.PortadaUrl,
                GenreId = vm.GenreId,
                ProducerId = vm.ProducerId,
                SecondaryGenres = new List<genre_serie>() 
            };

            
            if (vm.SecondaryGenresIds != null)
            {
                foreach (var genreId in vm.SecondaryGenresIds)
                {
                    serie.SecondaryGenres.Add(new genre_serie
                    {
                        GenreId = genreId, 
                        SerieId = serie.Id 
                    });
                }
            }

           
            await _repository.AddAsync(serie); 
        }


        public async Task Delete(int id)
        {
            var serie = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(serie);
        }

        public async Task<SaveSerieViewModel> GetByIdForEditAsync(int id)
        {
            var serie = await _repository.Series
                .Include(s => s.Genre) 
                .Include(s => s.Producer) 
                .Include(s => s.SecondaryGenres) 
                    .ThenInclude(gs => gs.Genre) 
                .Where(s => s.Id == id) 
                .FirstOrDefaultAsync(); 

            if (serie == null)
            {
                return null;
            }

            
            var saveSerieViewModel = new SaveSerieViewModel
            {
                Id = serie.Id,
                Title = serie.Title,
                Description = serie.Description,
                VideoUrl = serie.VideoUrl,
                PortadaUrl = serie.PortadaUrl,
                GenreId = serie.GenreId,
                ProducerId = serie.ProducerId,
                SecondaryGenresIds = serie.SecondaryGenres.Select(gs => gs.GenreId).ToList(), 
            };

            return saveSerieViewModel;
        }









        public async Task<List<SerieViewModel>> GetAllSeriesWithGenresAndProducerAsync()
        {
            var seriesWithGenresAndProducer = await _repository.Series
                .Include(s => s.Genre) 
                .Include(s => s.Producer)
                .Include(s => s.SecondaryGenres) 
                    .ThenInclude(gs => gs.Genre) 
                .Select(s => new SerieViewModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    PortadaUrl = s.PortadaUrl,
                    GenreId = s.GenreId,
                    GenreName = s.Genre.Name,
                    ProducerId = s.ProducerId,
                    ProducerName = s.Producer.Name,
                    Genres = s.SecondaryGenres.Select(gs => gs.Genre.Name).ToList() 
                })
                .ToListAsync();

            return seriesWithGenresAndProducer;
        }

        public async Task<List<SerieViewModel>> FilterSeriesAsync(int? genreId, int? producerId)
        {
            var query = _repository.Series
                .Include(s => s.Genre)  
                .Include(s => s.Producer)  
                .Include(s => s.SecondaryGenres)  
                .ThenInclude(gs => gs.Genre) 
                .AsQueryable();

            if (genreId.HasValue)
            {
                query = query.Where(s => s.GenreId == genreId.Value || s.SecondaryGenres.Any(g => g.GenreId == genreId.Value));
            }

            if (producerId.HasValue)
            {
                query = query.Where(s => s.ProducerId == producerId.Value);
            }

            var filteredSeries = await query
                .Select(s => new SerieViewModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    PortadaUrl = s.PortadaUrl,
                    GenreName = s.Genre.Name,  
                    ProducerName = s.Producer.Name,  
                                                     
                    Genres = s.SecondaryGenres.Select(gs => gs.Genre.Name).ToList()
                }).ToListAsync();

            return filteredSeries;
        }

    
        public async Task<List<Genre>> GetAllGenresAsync()
        {
            return await _repository.GetAllGenreAsync();
        }

     
        public async Task<List<Producer>> GetAllProducersAsync()
        {
            return await _repository.GetAllProducersAsync();
        }


        public async Task<List<SerieViewModel>> SearchSeriesAsync(string searchQuery)
        {
            return await _repository.Series
                .Where(s => s.Title.Contains(searchQuery))
                .Select(s => new SerieViewModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    PortadaUrl = s.PortadaUrl,
                    GenreName = s.Genre.Name,
                    ProducerName = s.Producer.Name,
                    Genres = s.SecondaryGenres.Select(gs => gs.Genre.Name).ToList()
                })
                .ToListAsync();
        }

        public async Task<SerieViewModel> GetSerieByIdAsync(int id)
        {
            var serie = await _repository.Series
            .Include(s => s.Genre)
            .Include(s => s.Producer)
            .Include(s => s.SecondaryGenres)
                .ThenInclude(gs => gs.Genre)
            .FirstOrDefaultAsync(s => s.Id == id);

            if (serie == null)
            {
                return null;
            }


            var serieViewModel = new SerieViewModel
            {
                Id = serie.Id,
                Title = serie.Title,
                Description = serie.Description,
                VideoUrl = serie.VideoUrl,
                PortadaUrl = serie.PortadaUrl,
                GenreId = serie.GenreId,
                ProducerId = serie.ProducerId,
                ProducerName = serie.Producer?.Name ?? "Unknown Producer",
                GenreName = serie.Genre?.Name ?? "Unknown Genre",
                Genres = serie.SecondaryGenres?.Where(gs => gs.Genre != null)
                                   .Select(gs => gs.Genre.Name)
                                   .ToList() ?? new List<string>()
            };

            return serieViewModel;
        }

    }
}
