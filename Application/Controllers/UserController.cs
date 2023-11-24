
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class TicketController : ControllerBase
{
    private readonly IMediator _mediator;
    public TicketController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("{id}")] // GET /ticket/id
    public async Task<IResult> GetUserById(int id, CancellationToken token)
    {
        Console.WriteLine("UserController.GetUserByIdAndName");
        return Results.Ok(null);
    }
    [HttpGet("[action]")] //placeholder for action name
    public async Task<IResult> GiveOut(CancellationToken token)
    {
        var ticket = new GiveOutAviaTicketCommand()
        {
            Status = "1",
            Ticket = "2",
            Size = "3"
        };

        var result = await _mediator.Send(ticket, token);
        return Results.Ok(null);
    }
}