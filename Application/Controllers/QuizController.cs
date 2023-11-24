using Application.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[Route("[controller]")]
[ApiController]
public class QuizController : ControllerBase
{
    private readonly IMediator _mediator;
    public QuizController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // Get all quizzes
    [HttpGet]
    public async Task<IResult> GetAllQuizzes(int moduleId, CancellationToken token)
    {
        try
        {
            var getAll = new GetQuizzesQuery(moduleId);
            var result = await _mediator.Send(getAll, token);
            return Results.Ok(result);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    // Create a new quiz
    [HttpPost]
    public async Task<IResult> CreateQuiz([FromBody] CreateQuizCommand createQuizCommand, CancellationToken token)
    {
        try
        {
            var result = await _mediator.Send(createQuizCommand, token);
            return Results.Ok(result);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
}