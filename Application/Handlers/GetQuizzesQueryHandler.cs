using MediatR;
using O2GEN.Models;

namespace Application.Handlers;

public class GetQuizzesQueryHandler : IRequestHandler<GetQuizzesQuery, IEnumerable<QuizDTO>>
{
    private readonly IQuizRepository _quizRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public GetQuizzesQueryHandler(IQuizRepository quizRepository, IUnitOfWork unitOfWork)
    {
        _quizRepository = quizRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<QuizDTO>> Handle(GetQuizzesQuery request, CancellationToken cancellationToken)
    {
        var quizzes = await _quizRepository.GetAllAsync(request.ModuleId, cancellationToken);
        
        if (!quizzes.Any())
            throw new System.Exception("No quizzes found in the repository");

        return quizzes.Select(quiz => new QuizDTO
        {
            Id = quiz.Id,
            ModuleID = quiz.ModuleID,
            Question = quiz.Question,
            Options = quiz.Options,
            CorrectOption = quiz.CorrectOption,
            Explanation = quiz.Explanation
        });
    }
}

public record GetQuizzesQuery(
    int ModuleId) : IRequest<IEnumerable<QuizDTO>>;