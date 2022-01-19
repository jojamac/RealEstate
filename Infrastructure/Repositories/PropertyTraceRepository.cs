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
    public class PropertyTraceRepository : IPropertyTraceRepository
    {
        private readonly IRepositoryAsync<PropertyTrace> _repository;
        private readonly IDistributedCache _distributedCache;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="distributedCache"></param>
        /// <param name="repository"></param>
        public PropertyTraceRepository(IDistributedCache distributedCache, IRepositoryAsync<PropertyTrace> repository)
        {
            _repository = repository;
            _distributedCache = distributedCache;

        }

        /// <summary>
        /// 
        /// </summary>
        public IQueryable<PropertyTrace> PropertyTraces => _repository.Entities;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyTrace"></param>
        /// <returns></returns>
        public async Task DeleteAsync(PropertyTrace propertyTrace)
        {
            await _repository.DeleteAsync(propertyTrace);
            await _distributedCache.RemoveAsync(CacheKeys.PropertyTraceCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.PropertyTraceCacheKeys.GetKey(propertyTrace.Id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyTraceId"></param>
        /// <returns></returns>
        public async Task<PropertyTrace> GetByIdAsync(int idProperty)
        {
            return await _repository.Entities.Where(p => p.IdProperty == idProperty).FirstOrDefaultAsync();
        }

        public async Task<PropertyTrace> GetByIdPropertyTrace(int id)
        {
            return await _repository.Entities.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<PropertyTrace>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(PropertyTrace propertyTrace)
        {
            await _repository.AddAsync(propertyTrace);
            await _distributedCache.RemoveAsync(CacheKeys.PropertyTraceCacheKeys.ListKey);
            return propertyTrace.Id;
        }

        public async Task UpdateAsync(PropertyTrace propertyTrace)
        {
            await _repository.UpdateAsync(propertyTrace);
            await _distributedCache.RemoveAsync(CacheKeys.PropertyTraceCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.PropertyTraceCacheKeys.GetKey(propertyTrace.Id));
        }
    }
}
