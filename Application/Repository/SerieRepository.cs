using Database.Contexts;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository
{
    public class SerieRepository
    {

        private readonly ApplicationContext _dbContext;

        public SerieRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Serie> Series => _dbContext.Series;
        

        public async Task AddAsync(Serie serie)
        {
            await _dbContext.Series.AddAsync(serie);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Serie serie)
        {
            _dbContext.Entry(serie).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateGenresAsync(Serie serie)
        {
           
            if (serie == null || serie.SecondaryGenresIds == null)
            {
                throw new ArgumentNullException(nameof(serie), "La serie o su lista de géneros secundarios no puede ser nula.");
            }

           
            var existingGenreRelations = await _dbContext.genre_Series
                .Where(gs => gs.SerieId == serie.Id)
                .ToListAsync();

           
            var existingGenreIds = existingGenreRelations.Select(gs => gs.GenreId).ToList();

            
            var genresToRemove = existingGenreRelations
                .Where(gs => !serie.SecondaryGenresIds.Contains(gs.GenreId))
                .ToList();

            if (genresToRemove.Any())
            {
                _dbContext.genre_Series.RemoveRange(genresToRemove);
                await _dbContext.SaveChangesAsync(); 
            }

            
            foreach (var genreId in serie.SecondaryGenresIds)
            {
                
                if (!existingGenreIds.Contains(genreId))
                {
                    var newRelation = new genre_serie
                    {
                        SerieId = serie.Id,
                        GenreId = genreId
                    };
                    await _dbContext.genre_Series.AddAsync(newRelation);
                }
            }

           
            await _dbContext.SaveChangesAsync();
        }





        public async Task DeleteAsync(Serie serie)
        {
           _dbContext.Set<Serie>().Remove(serie);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Serie>> GetAllAsync()
        {
            return await _dbContext.Set<Serie>().ToListAsync();
        }



        public async Task<List<Genre>> GetAllGenreAsync()
        {
            return await _dbContext.Genres.ToListAsync();
        }

        public async Task<List<Producer>> GetAllProducersAsync()
        {
            return await _dbContext.Producers.ToListAsync();
        }


        public async Task<Serie> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Serie>().FindAsync(id);
        }


    }
}
