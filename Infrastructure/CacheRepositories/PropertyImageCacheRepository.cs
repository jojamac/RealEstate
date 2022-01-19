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
using Infrastructure.CacheKeys;

namespace Infrastructure.CacheRepositories
{
    public class PropertyImageCacheRepository : IPropertyImageCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IPropertyImageRepository _propertyImageRepository;

        public PropertyImageCacheRepository(IDistributedCache distributedCache, IPropertyImageRepository propertyImageRepository)
        {
            _distributedCache = distributedCache;
            _propertyImageRepository = propertyImageRepository;
        }

        public async Task<PropertyImage> GetByIdAsync(int propertyImageId)
        {
            string cacheKey = PropertyImageCacheKeys.GetKey(propertyImageId);
            var propertyImage = await _distributedCache.GetAsync<PropertyImage>(cacheKey);
            if (propertyImage == null)
            {
                propertyImage = await _propertyImageRepository.GetByIdAsync(propertyImageId);
                Throw.Exception.IfNull(propertyImage, "propertyImage", "Property Image No Found");
                await _distributedCache.SetAsync(cacheKey, propertyImage);
            }
            return propertyImage;
        }

        public async Task<List<PropertyImage>> GetByIdProperty(int IdProperty)
        {
            string cacheKey = PropertyImageCacheKeys.GetKey(IdProperty);
            var propertyImage = await _distributedCache.GetAsync<List<PropertyImage>>(cacheKey);
            if(propertyImage == null)
            {
                propertyImage = await _propertyImageRepository.GetByIdPropertyAsync(IdProperty);
                Throw.Exception.IfNull(propertyImage, "propertyImage", "Property Image No Found");
                await _distributedCache.SetAsync(cacheKey, propertyImage);
            }
            return propertyImage;
        }

        public async Task<List<PropertyImage>> GetCachedListAsync()
        {
            string cacheKey = PropertyImageCacheKeys.ListKey;
            var propertyImageList = await _distributedCache.GetAsync<List<PropertyImage>>(cacheKey);

            if (propertyImageList == null)
            {
                propertyImageList = await _propertyImageRepository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, propertyImageList);
            }

            return propertyImageList;
        }
    }
}
