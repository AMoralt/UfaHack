
using Domain.AggregationModels;
using O2GEN.Models;

public interface ICoursesRepository : IRepository<Courses>
{
    Task<int> CreateAsync(Courses itemToCreate, CancellationToken cancellationToken = default);
    Task<IEnumerable<Courses>> GetAllAsync(int limit, int offset, CancellationToken cancellationToken = default);
    Task<Courses> GetAsync(int id, CancellationToken cancellationToken = default);
    Task UpdateAsync(Courses itemToUpdate, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(Courses itemToDelete, CancellationToken cancellationToken = default);
}