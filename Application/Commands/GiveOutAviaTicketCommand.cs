
using MediatR;

public class GiveOutAviaTicketCommand : IRequest
{
    public string Status { get;set; }
    public string Ticket { get;set; }
    public string Size { get;set; }
}