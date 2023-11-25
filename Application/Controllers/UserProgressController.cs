using Application.Commands;
using Application.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserProgressController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserProgressController(IMediator mediator)
    {
        _mediator = mediator;
    }

    
    [HttpPost]
    public async Task<IResult> Create([FromBody] CreateUserProgressCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Results.Ok(result);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    
    [HttpGet("CourseCompletionRate/{courseId}")]
    public async  Task<IResult> GetCourseCompletionRate(int courseId, CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetCourseCompletionQuery(courseId);
            var completionRate = await _mediator.Send(query, cancellationToken);
            return Results.Ok(completionRate);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    [HttpGet("ModuleCompletionRate/{moduleId}")]
    public async  Task<IResult> GetModuleCompletionRate(int moduleId, int userId, CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetModuleCompletionQuery(moduleId,userId);
            var completionRate = await _mediator.Send(query, cancellationToken);
            return Results.Ok(completionRate);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    
    [HttpPut]
    public async Task<IResult> Update([FromBody] UpdateUserProgressCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result ? Results.Ok("Update successful") : Results.BadRequest("Update failed");
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
}