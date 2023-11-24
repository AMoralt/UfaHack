using Application.Exception;
using Domain.AggregationModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly IMediator _mediator;
    public CourseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IResult> GetAllCourses(int limit, int offset, CancellationToken token)
    {
        try
        {
            var getAll = new GetCoursesQuery(limit, offset);
            var result = await _mediator.Send(getAll, token);
            return Results.Ok(result);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    [HttpPost]
    public async Task<IResult> CreateCourse([FromBody] CreateCourseCommand createCourseCommand, CancellationToken token)
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