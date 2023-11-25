using Infrastructure.Contracts;
using MediatR;

namespace Application.Handlers;

public class GetModuleCompletionQueryHandler : IRequestHandler<GetModuleCompletionQuery, double>
{
    private readonly IUserProgressRepository _userProgressRepository;
    private readonly IQuizRepository _quizRepository;

    public GetModuleCompletionQueryHandler(IUserProgressRepository userProgressRepository, IQuizRepository quizRepository)
    {
        _userProgressRepository = userProgressRepository;
        _quizRepository = quizRepository;
    }

    public async Task<double> Handle(GetModuleCompletionQuery request, CancellationToken cancellationToken)
    {
        
        var totalQuiz = await _quizRepository.GetAllAsync(request.ModuleId, cancellationToken);
        var totalQuizCount = totalQuiz.Count();
        
        var completedQuiz = await _userProgressRepository.GetCompletedQuizCount(request.ModuleId,request.userId, cancellationToken);

        if (totalQuizCount == 0)
        {
            return 0; 
        }

        var completionPercentage = (double)completedQuiz / totalQuizCount * 100;
        return completionPercentage;
    }
}

public record GetModuleCompletionQuery(int ModuleId, int userId) : IRequest<double>;