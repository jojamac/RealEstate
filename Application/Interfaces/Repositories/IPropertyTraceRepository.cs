using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IPropertyTraceRepository
    {
        IQueryable<PropertyTrace> PropertyTraces { get; }

        Task<List<PropertyTrace>> GetListAsync();

        Task<PropertyTrace> GetByIdAsync(int idProperty);

        Task<PropertyTrace> GetByIdPropertyTrace(int id);

        Task<int> InsertAsync(PropertyTrace propertyTrace);

        Task UpdateAsync(PropertyTrace propertyTrace);

        Task DeleteAsync(PropertyTrace propertyTrace);
    }
}
