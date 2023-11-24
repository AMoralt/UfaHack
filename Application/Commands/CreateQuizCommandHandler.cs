using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Exception;
using Application.Service;
using Domain.AggregationModels;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using O2GEN.Authorization;
using O2GEN.Models;

public class CreateQuizCommandHandler : IRequestHandler<CreateQuizCommand, QuizDTO>
{
    private readonly IQuizRepository _quizzesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateQuizCommandHandler(IQuizRepository quizzesRepository, IUnitOfWork unitOfWork)
    {
        _quizzesRepository = quizzesRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<QuizDTO> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.StartTransaction(cancellationToken);

        var quiz = new Quiz()
        {
            ModuleID = request.ModuleID,
            Question = request.Question,
            Options = request.Options,
            CorrectOption = request.CorrectOption,
            Explanation = request.Explanation
        };

        var resultId = await _quizzesRepository.CreateAsync(quiz, cancellationToken);

        QuizDTO quizDto = new QuizDTO
        {
            Id = resultId,
            ModuleID = request.ModuleID,
            Question = request.Question,
            Options = request.Options,
            CorrectOption = request.CorrectOption,
            Explanation = request.Explanation
        };

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return quizDto;
    }
}

public record CreateQuizCommand(
    int ModuleID, 
    string Question, 
    string[] Options, 
    int[] CorrectOption,
    string Explanation) : IRequest<QuizDTO>;