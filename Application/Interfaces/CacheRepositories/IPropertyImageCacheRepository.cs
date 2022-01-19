using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.CacheRepositories
{
    public interface IPropertyImageCacheRepository
    {
        Task<List<PropertyImage>> GetCachedListAsync();

        Task<PropertyImage> GetByIdAsync(int propertyImageId);

        Task<List<PropertyImage>> GetByIdProperty(int IdProperty);
    }
}
