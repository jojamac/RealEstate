using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IPropertyImageRepository
    {
        IQueryable<PropertyImage> PropertyImages { get; }

        Task<List<PropertyImage>> GetListAsync();

        Task<PropertyImage> GetByIdAsync(int propertyImageId);

        Task<List<PropertyImage>> GetByIdPropertyAsync(int IdProperty);

        Task<int> InsertAsync(PropertyImage propertyImage);

        Task UpdateAsync(PropertyImage propertyImage);

        Task DeleteAsync(PropertyImage propertyImage);
    }
}
