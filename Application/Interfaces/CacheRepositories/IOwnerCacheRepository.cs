using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.CacheRepositories
{
    public interface IOwnerCacheRepository
    {
        Task<List<Owner>> GetCachedListAsync();

        Task<Owner> GetByIdAsync(int ownerId);
    }
}
