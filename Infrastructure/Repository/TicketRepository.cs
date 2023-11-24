
using Dapper;
using Npgsql;

public class TicketRepository : ITicketRepository
{
    public TicketRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }
    private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;

    public Task CreateAsync(AviaTicket itemToCreate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    public async Task<IQueryable<AviaTicket>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        const string sql = @"SELECT 5";
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        var result = await connection.ExecuteAsync(sql);
        return null;
    }

    public Task UpdateAsync(AviaTicket itemToUpdate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(AviaTicket itemToDelete, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}