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
    public class PropertyImageRepository : IPropertyImageRepository
    {
        private readonly IRepositoryAsync<PropertyImage> _repository;
        private readonly IDistributedCache _distributedCache;

        public PropertyImageRepository(IDistributedCache distributedCache, IRepositoryAsync<PropertyImage> repository)
        {
            _repository = repository;
            _distributedCache = distributedCache;
        }

        public IQueryable<PropertyImage> PropertyImages => _repository.Entities;

        public async Task DeleteAsync(PropertyImage propertyImage)
        {
            await _repository.DeleteAsync(propertyImage);
            await _distributedCache.RemoveAsync(CacheKeys.PropertyImageCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.PropertyImageCacheKeys.GetKey(propertyImage.Id));
        }

        public async Task<PropertyImage> GetByIdAsync(int propertyImageId)
        {
            return await _repository.Entities.Where(p => p.Id == propertyImageId).FirstOrDefaultAsync();
        }

        public async Task<List<PropertyImage>> GetByIdPropertyAsync(int IdProperty)
        {
            return await _repository.Entities.Where(p => p.IdProperty == IdProperty).ToListAsync();
        }

        public async Task<List<PropertyImage>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(PropertyImage propertyImage)
        {
            await _repository.AddAsync(propertyImage);
            await _distributedCache.RemoveAsync(CacheKeys.PropertyImageCacheKeys.ListKey);
            return propertyImage.Id;
        }

        public async Task UpdateAsync(PropertyImage propertyImage)
        {
            await _repository.UpdateAsync(propertyImage);
            await _distributedCache.RemoveAsync(CacheKeys.PropertyImageCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.PropertyImageCacheKeys.GetKey(propertyImage.Id));
        }
    }
}
