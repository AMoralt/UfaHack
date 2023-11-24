using Application.Exception;
using Domain.AggregationModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class ModuleController : ControllerBase
{
    private readonly IMediator _mediator;
    public ModuleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IResult> GetAllModules(int id, CancellationToken token)
    {
        try
        {
            var getAll = new GetModulesQuery(id);
            var result = await _mediator.Send(getAll, token);
            return Results.Ok(result);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    [HttpPost]
    public async Task<IResult> CreateModule([FromBody] CreateCourseCommand createCourseCommand, CancellationToken token)
    {
        try
        {
            var result = await _mediator.Send(createCourseCommand, token);
            return Results.Ok(result);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
}