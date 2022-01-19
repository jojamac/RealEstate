using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IOwnerRepository
    {
        IQueryable<Owner> Owners { get; }

        Task<List<Owner>> GetListAsync();

        Task<Owner> GetByIdAsync(int ownerId);

        Task<int> InsertAsync(Owner owner);

        Task UpdateAsync(Owner owner);

        Task DeleteAsync(Owner owner);
    }
}
