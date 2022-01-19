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
    public class PropertyTraceCacheRepository : IPropertyTraceCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IPropertyTraceRepository _propertyTraceRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="distributedCache"></param>
        /// <param name="propertyTraceRepository"></param>
        public PropertyTraceCacheRepository(IDistributedCache distributedCache, IPropertyTraceRepository propertyTraceRepository)
        {
            _distributedCache = distributedCache;
            _propertyTraceRepository = propertyTraceRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyId"></param>
        /// <returns></returns>
        public async Task<PropertyTrace> GetByIdAsync(int propertyId)
        {
            string cacheKey = PropertyTraceCacheKeys.GetKey(propertyId);
            var propertyTrace = await _distributedCache.GetAsync<PropertyTrace>(cacheKey);
            if (propertyTrace == null)
            {
                propertyTrace = await _propertyTraceRepository.GetByIdAsync(propertyId);
                Throw.Exception.IfNull(propertyTrace, "PropertyTrace", " Property Trace No  Found");
                await _distributedCache.SetAsync(cacheKey, propertyTrace);
            }
            return propertyTrace;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<PropertyTrace>> GetCachedListAsync()
        {
            string cacheKey = PropertyTraceCacheKeys.ListKey;
            var propertyTraceList = await _distributedCache.GetAsync<List<PropertyTrace>>(cacheKey);

            if (propertyTraceList == null)
            {
                propertyTraceList = await _propertyTraceRepository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, propertyTraceList);
            }

            return propertyTraceList;
        }
    }
}
