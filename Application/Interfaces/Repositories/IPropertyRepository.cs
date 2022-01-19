using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IPropertyRepository
    {
        IQueryable<Property> Properties { get; }

        Task<List<Property>> GetListAsync();

        Task<Property> GetByIdAsync(int propertyId);

        Task<int> InsertAsync(Property property);

        Task UpdateAsync(Property property);

        Task DeleteAsync(Property property);
    }
}
