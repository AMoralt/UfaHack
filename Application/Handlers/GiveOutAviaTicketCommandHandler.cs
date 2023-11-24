using MediatR;

public class GiveOutAviaTicketCommandHandler : IRequestHandler<GiveOutAviaTicketCommand>
{
    private readonly ITicketRepository _aviaTicketRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GiveOutAviaTicketCommandHandler(ITicketRepository aviaTicketRepository, IUnitOfWork unitOfWork)
    {
        _aviaTicketRepository = aviaTicketRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(GiveOutAviaTicketCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.StartTransaction(cancellationToken);
        var aviaTicket = await _aviaTicketRepository.GetAllAsync();
        //ASAP: track all tickets by Tracker!!!
        
        if (aviaTicket is null) 
            throw new System.Exception("Ticket not found");
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}