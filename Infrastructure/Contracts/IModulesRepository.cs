
using Domain.AggregationModels;
using O2GEN.Models;

public interface IModulesRepository : IRepository<Modules>
{
    Task<int> CreateAsync(Modules itemToCreate, CancellationToken cancellationToken = default);
    Task<IEnumerable<Modules>> GetAllAsync(int id, CancellationToken cancellationToken = default);
    Task UpdateAsync(Modules itemToUpdate, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(Modules itemToDelete, CancellationToken cancellationToken = default);
}