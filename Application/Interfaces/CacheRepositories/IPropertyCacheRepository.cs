using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.CacheRepositories
{
    public interface IPropertyCacheRepository
    {
        Task<List<Property>> GetCachedListAsync();

        Task<Property> GetByIdAsync(int propertyId);
    }
}
