
using Domain.AggregationModels;
using O2GEN.Models;

public interface ICredentialRepository : IRepository<CredentialsDTO>
{
    Task<int> CreateAsync(UserData itemToCreate, CancellationToken cancellationToken = default);
    Task<IQueryable<UserData>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<UserData> GetAsync(string login, string password, CancellationToken cancellationToken = default);
    Task<UserData> GetAsync(string login, CancellationToken cancellationToken = default);
    Task UpdateAsync(UserData itemToUpdate, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(UserData itemToDelete, CancellationToken cancellationToken = default);
}