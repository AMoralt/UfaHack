using Application.Exception;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    [Route("Account/Login")]
    [HttpPost]
    public async Task<IResult> Login([FromBody] LoginQuery loginData,CancellationToken token)
    {
        try
        {
            var result = await _mediator.Send(loginData, token);

            return Results.Json(result);
        }
        catch (UnauthorizedException e)
        {
            return Results.Unauthorized();
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    [Route("Account/SignIn")]
    [HttpPost]
    public async Task<IResult> Signin([FromBody] SignupQuery signinData,CancellationToken token)
    {
        try
        {
            var result = await _mediator.Send(signinData, token);

            if (result is null)
                return Results.BadRequest();

            return Results.Ok();
        }
        catch (UnauthorizedException e)
        {
            return Results.Unauthorized();
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
}