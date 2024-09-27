using Application.Repository;
using Application.ViewModels;
using Database.Contexts;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProducerService
    {
        private readonly ProducerRepository _repository;

        public ProducerService(ApplicationContext dbContext)
        {
            _repository = new ProducerRepository(dbContext);
        }

      
        public async Task AddProducerAsync(SaveProducerViewModel vm)
        {
            Producer producer = new()
            {
                Name = vm.Name
            };

            await _repository.AddAsync(producer);
        }

        
        public async Task UpdateProducerAsync(SaveProducerViewModel vm)
        {
            Producer producer = new()
            {
                Id = vm.Id,
                Name = vm.Name
            };

            await _repository.UpdateAsync(producer);
        }

      
        public async Task DeleteProducerAsync(int producerId)
        {
            var producer = await _repository.GetByIdAsync(producerId);

            if (producer == null)
            {
                throw new Exception("Productora no encontrada");
            }

            await _repository.DeleteAsync(producer);
        }

        
        public async Task<List<ProducerViewModel>> GetAllProducersAsync()
        {
            var producers = await _repository.GetAllAsync();
            return producers.Select(p => new ProducerViewModel
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
        }

    
        public async Task<List<SaveProducerViewModel>> SearchProducersAsync(string searchQuery)
        {
            var producers = await _repository.Producers
                .Where(p => p.Name.Contains(searchQuery))
                .ToListAsync();

            return producers.Select(p => new SaveProducerViewModel
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
        }

      
        public async Task<SaveProducerViewModel> GetProducerByIdAsync(int producerId)
        {
            var producer = await _repository.GetByIdAsync(producerId);
            if (producer == null) return null;

            return new SaveProducerViewModel
            {
                Id = producer.Id,
                Name = producer.Name
            };
        }
    }
}
