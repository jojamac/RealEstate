using Application.CacheKeys;
using Application.Interfaces.CacheRepositories;
using Application.Interfaces.Repositories;
using AspNetCoreHero.ThrowR;
using Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using AspNetCoreHero.Extensions.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CacheRepositories
{
    public class PropertyCacheRepository: IPropertyCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IPropertyRepository _propertyRepository;

        public PropertyCacheRepository(IDistributedCache distributedCache, IPropertyRepository propertyRepository)
        {
            _distributedCache = distributedCache;
            _propertyRepository = propertyRepository;
        }

        public async Task<Property> GetByIdAsync(int propertyId)
        {
            string cacheKey = PropertyCacheKeys.GetKey(propertyId);
            var property = await _distributedCache.GetAsync<Property>(cacheKey);
            if (property == null)
            {
                property = await _propertyRepository.GetByIdAsync(propertyId);
                Throw.Exception.IfNull(property, "Property", "No Property Found");
                await _distributedCache.SetAsync(cacheKey, property);
            }
            return property;
        }

        public async Task<List<Property>> GetCachedListAsync()
        {
            string cacheKey = PropertyCacheKeys.ListKey;
            var propertyList = await _distributedCache.GetAsync<List<Property>>(cacheKey);

            if (propertyList == null)
            {
                propertyList = await _propertyRepository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, propertyList);
            }

            return propertyList;
        }
    }
}
