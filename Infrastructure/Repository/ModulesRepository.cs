using Dapper;
using Domain.AggregationModels;
using Npgsql;
using O2GEN.Models;

public class ModulesRepository : IModulesRepository
{
    public ModulesRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }
    private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;

    public async Task<int> CreateAsync(Modules itemToCreate, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            INSERT INTO modules (courseid, title, description, moduleorder)
            VALUES 
                (@courseid, @title, @description, @moduleorder)
            RETURNING moduleid;
            ";
        var parameters = new
        {
            title = itemToCreate.Title,
            description = itemToCreate.Description,
            courseid = itemToCreate.CourseID,
            moduleorder = itemToCreate.ModuleOrder
        };
        
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        var Id = await connection.ExecuteScalarAsync(sql, param: parameters);

        return (int)Id;
    }

    public async Task<IEnumerable<Modules>> GetAllAsync(int id, CancellationToken cancellationToken = default)
    {
        string sql = @"
            SELECT moduleid AS Id, courseid, title, description, moduleorder
            FROM modules
            WHERE courseid = @courseid";
        
        var parameters = new
        {
            courseid = id
        };
        
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        var result =  await connection.QueryAsync<Modules>(sql, param: parameters);
        
        return result;
    }

    public Task<Modules> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Modules itemToUpdate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(Modules itemToDelete, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}