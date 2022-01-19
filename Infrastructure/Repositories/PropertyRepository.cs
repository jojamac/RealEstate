using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly IRepositoryAsync<Property> _repository;
        private readonly IDistributedCache _distributedCache;

        public PropertyRepository(IDistributedCache distributedCache,IRepositoryAsync<Property> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Property> Properties => _repository.Entities;

        public async Task DeleteAsync(Property property)
        {
            await _repository.DeleteAsync(property);
            await _distributedCache.RemoveAsync(CacheKeys.PropertyCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.PropertyCacheKeys.GetKey(property.Id));

        }

        public async Task<Property> GetByIdAsync(int propertyId)
        {
            return await _repository.Entities.Where(p => p.Id == propertyId).FirstOrDefaultAsync();
        }

        public async Task<List<Property>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Property property)
        {
            await _repository.AddAsync(property);
            await _distributedCache.RemoveAsync(CacheKeys.PropertyCacheKeys.ListKey);
            return property.Id;
        }

        public async Task UpdateAsync(Property property)
        {
            await _repository.UpdateAsync(property);
            await _distributedCache.RemoveAsync(CacheKeys.PropertyCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.PropertyCacheKeys.GetKey(property.Id));
        }
    }
}
