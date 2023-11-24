using Dapper;
using Domain.AggregationModels;
using Npgsql;
using O2GEN.Models;

public class CredentialRepository : ICredentialRepository
{
    public CredentialRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }
    private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;

    public Task CreateAsync(UserData itemToCreate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    public async Task<IQueryable<UserData>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        // const string sql = @"SELECT 5";
        // var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        // var result = await connection.ExecuteAsync(sql);
        throw new NotImplementedException();
    }
    public async Task<UserData> GetAsync(string login, string password, CancellationToken cancellationToken = default)
    {
        return null;
    }

    public Task<UserData> GetAsync(string login, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(UserData itemToUpdate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(UserData itemToDelete, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}