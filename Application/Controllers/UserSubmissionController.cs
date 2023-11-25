using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserSubmissionController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserSubmissionController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    // POST api/UserSubmission
    [HttpPost]
    public async Task<IResult> Create([FromBody] CreateUserSubmissionCommand command, CancellationToken token)
    {
        try
        {
            var result = await _mediator.Send(command, token);
            return Results.Ok(result);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
}