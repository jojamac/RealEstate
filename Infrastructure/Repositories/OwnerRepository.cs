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
    public class OwnerRepository : IOwnerRepository
    {
        private readonly IRepositoryAsync<Owner> _repository;
        private readonly IDistributedCache _distributedCache;


        public OwnerRepository(IDistributedCache distributedCache, IRepositoryAsync<Owner> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Owner> Owners => _repository.Entities;

        public async Task DeleteAsync(Owner owner)
        {
            await _repository.DeleteAsync(owner);
            await _distributedCache.RemoveAsync(CacheKeys.OwnerCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.OwnerCacheKeys.GetKey(owner.Id));
        }

        public async Task<Owner> GetByIdAsync(int ownerId)
        {
            return await _repository.Entities.Where(p => p.Id == ownerId).FirstOrDefaultAsync();
        }

        public async Task<List<Owner>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Owner owner)
        {
            await _repository.AddAsync(owner);
            await _distributedCache.RemoveAsync(CacheKeys.OwnerCacheKeys.ListKey);
            return owner.Id;
        }

        public async Task UpdateAsync(Owner owner)
        {
            await _repository.UpdateAsync(owner);
            await _distributedCache.RemoveAsync(CacheKeys.OwnerCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.OwnerCacheKeys.GetKey(owner.Id));
        }
    }
}
