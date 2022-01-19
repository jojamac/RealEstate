using Application.Interfaces.CacheRepositories;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.CacheKeys;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Extensions.Caching;
using AspNetCoreHero.ThrowR;

namespace Infrastructure.CacheRepositories
{
    public class OwnerCacheRepository : IOwnerCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IOwnerRepository  _ownerRepository;


        public OwnerCacheRepository(IDistributedCache distributedCache, IOwnerRepository ownerRepository)
        {
            _distributedCache = distributedCache;
            _ownerRepository = ownerRepository;
        }

        public async Task<Owner> GetByIdAsync(int ownerId)
        {
            string cacheKey = OwnerCacheKeys.GetKey(ownerId);
            var owner = await _distributedCache.GetAsync<Owner>(cacheKey);
            if (owner == null)
            {
                owner = await _ownerRepository.GetByIdAsync(ownerId);
                Throw.Exception.IfNull(owner, "Owner", "No Owner Found");
                await _distributedCache.SetAsync(cacheKey, ownerId);
            }
            return owner;
        }

        public async Task<List<Owner>> GetCachedListAsync()
        {
            string cacheKey = OwnerCacheKeys.ListKey;
            var ownerList = await _distributedCache.GetAsync<List<Owner>>(cacheKey);

            if (ownerList == null)
            {
                ownerList = await _ownerRepository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, ownerList);
            }

            return ownerList;
        }
    }
}
