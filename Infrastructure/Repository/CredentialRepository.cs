using Dapper;
using Domain.AggregationModels;
using Npgsql;
using O2GEN.Models;

public class CredentialRepository : ICredentialRepository
{
    private readonly string ENCR = "02GD89htgGbWEp98ubrg9oies:DGH:ORGojhg89_@jog;SOgh";
    public CredentialRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }
    private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;

    public async Task<int> CreateAsync(UserData itemToCreate, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            INSERT INTO Users (Login, Name, Email, Password)
            VALUES 
                (@login,@name,@email,pgp_sym_encrypt(@password, @en))
            RETURNING UserID;
            ";
        var parameters = new
        {
            login = itemToCreate.Login.ToLower(),
            name = itemToCreate.Name,
            email = itemToCreate.Email,
            password = itemToCreate.Password,
            en = $"'{ENCR}'"
        };
        
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        var Id = await connection.ExecuteScalarAsync(sql, param: parameters);

        return (int)Id;
    }
    public async Task<IQueryable<UserData>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    public async Task<UserData> GetAsync(string login, string password, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT UserID AS Id, Login, Name, Email, RegistrationDate, Role
            FROM Users AS u
            WHERE u.Login = @login AND encode(digest(pgp_sym_decrypt(cast(u.Password as bytea), @en), 'sha256'), 'hex') = encode(digest(@password, 'sha256'), 'hex')
            ";
        var parameters = new
        {
            password = password,
            login = login,
            en =  $"'{ENCR}'"
        };
        
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);

        var user = await connection
            .QuerySingleOrDefaultAsync<UserData>(sql, param: parameters);

        return user;
    }

    public async Task<UserData> GetAsync(string login, CancellationToken cancellationToken = default)
    {
        const string sql = $@"
            SELECT UserID AS Id, Login, Name, Email, RegistrationDate, Role
            FROM Users AS u
            WHERE u.Login = @login
            ";
        var parameters = new
        {
            login = login
        };
        
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);

        var user = await connection
            .QuerySingleOrDefaultAsync<UserData>(sql, param: parameters);

        return user;
    }

    public Task UpdateAsync(UserData itemToUpdate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<int> DeleteAsync(UserData itemToDelete, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            DELETE
            FROM Users AS u
            WHERE u.UserID = @id
            RETURNING UserID";
        
        var parameters = new { id = itemToDelete.Id };
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        var Id = await connection.ExecuteScalarAsync(sql, param: parameters);

        return (int)Id;
    }
}