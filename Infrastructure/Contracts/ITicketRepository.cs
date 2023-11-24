
public interface ITicketRepository : IRepository<AviaTicket>
{
    Task CreateAsync(AviaTicket itemToCreate, CancellationToken cancellationToken = default);
    Task<IQueryable<AviaTicket>> GetAllAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(AviaTicket itemToUpdate, CancellationToken cancellationToken = default);
    Task DeleteAsync(AviaTicket itemToDelete, CancellationToken cancellationToken = default);
}