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

        public async Task<Serie> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Serie>().FindAsync(id);
        }


    }
}
