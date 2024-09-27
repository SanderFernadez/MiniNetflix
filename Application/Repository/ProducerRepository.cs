using Database.Contexts;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Repository
{
    public class ProducerRepository
    {
        private readonly ApplicationContext _dbContext;

        public ProducerRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

       
        public IQueryable<Producer> Producers => _dbContext.Producers;

     
        public async Task AddAsync(Producer producer)
        {
            await _dbContext.Producers.AddAsync(producer);
            await _dbContext.SaveChangesAsync();
        }

       
        public async Task UpdateAsync(Producer producer)
        {
            _dbContext.Entry(producer).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

   
        public async Task DeleteAsync(Producer producer)
        {
            _dbContext.Producers.Remove(producer);
            await _dbContext.SaveChangesAsync();
        }

       
        public async Task<List<Producer>> GetAllAsync()
        {
            return await _dbContext.Producers.ToListAsync();
        }

       
        public async Task<Producer> GetByIdAsync(int id)
        {
            return await _dbContext.Producers.FindAsync(id);
        }
    }
}
