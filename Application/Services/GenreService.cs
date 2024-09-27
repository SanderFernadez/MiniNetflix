using Application.Repository;
using Application.ViewModels;
using Database.Contexts;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GenreService
    {
        private readonly GenreRepository _repository;

        public GenreService(ApplicationContext dbContext)
        {
            _repository = new GenreRepository(dbContext);
        }

       
        public async Task AddGenreAsync(SaveGenreViewModel vm)
        {
            Genre genre = new()
            {
                Name = vm.Name
            };

            await _repository.AddAsync(genre);
        }

        
        public async Task UpdateGenreByAsync(SaveGenreViewModel vm)
        {
            Genre genre = new()
            {
                Id = vm.Id,
                Name = vm.Name
            };

            await _repository.UpdateAsync(genre);
        }

       
        public async Task DeleteGenreAsync(int genreId)
        {
            var genre = await _repository.GetByIdAsync(genreId);

            if (genre == null)
            {
                throw new Exception("Género no encontrado");
            }

            await _repository.DeleteAsync(genre);
        }

       
        public async Task<List<GenreViewModel>> GetAllGenresAsync()
        {
            var genres = await _repository.GetAllAsync();
            return genres.Select(g => new GenreViewModel
            {
                Id = g.Id,
                Name = g.Name
            }).ToList();
        }

        
        public async Task<List<SaveGenreViewModel>> SearchGenresAsync(string searchQuery)
        {
            var genres = await _repository.Genres
                .Where(g => g.Name.Contains(searchQuery))
                .ToListAsync();

            return genres.Select(g => new SaveGenreViewModel
            {
                Id = g.Id,
                Name = g.Name
            }).ToList();
        }

        
        public async Task<SaveGenreViewModel> GetGenreByIdAsync(int genreId)
        {
            var genre = await _repository.GetByIdAsync(genreId);
            if (genre == null) return null;

            return new SaveGenreViewModel
            {
                Id = genre.Id,
                Name = genre.Name
            };
        }
    }
}
