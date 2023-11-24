using Application.Exception;
using Domain.AggregationModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [Route("User/Login")]
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
    [Route("User/SignUp")]
    [HttpPost]
    public async Task<IResult> SignUp([FromBody] SignupCommand signinData, CancellationToken token)
    {
        try
        {
            CredentialsDTO result = await _mediator.Send(signinData, token);

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
    [Route("User/Delete")]
    [HttpPost]
    public async Task<IResult> Delete([FromBody] SignupCommand signinData, CancellationToken token)
    {
        try
        {
            CredentialsDTO result = await _mediator.Send(signinData, token);

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
    [Authorize]
    [HttpGet]
    public async Task<IResult> Sign(CancellationToken token)
    {
        try
        {
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