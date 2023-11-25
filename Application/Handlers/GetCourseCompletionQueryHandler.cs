using Infrastructure.Contracts;
using MediatR;

namespace Application.Handlers;

public class GetCourseCompletionQueryHandler : IRequestHandler<GetCourseCompletionQuery, double>
{
    private readonly IUserProgressRepository _userProgressRepository;

    public GetCourseCompletionQueryHandler(IUserProgressRepository userProgressRepository)
    {
        _userProgressRepository = userProgressRepository;
    }

    public async Task<double> Handle(GetCourseCompletionQuery request, CancellationToken cancellationToken)
    {
        
        var totalModules = await _userProgressRepository.GetTotalModulesCount(request.CourseID, cancellationToken);

        
        var completedModules = await _userProgressRepository.GetCompletedModulesCount(request.CourseID, cancellationToken);

        if (totalModules == 0)
        {
            return 0; 
        }

        var completionPercentage = (double)completedModules / totalModules * 100;
        return completionPercentage;
    }
}

public record GetCourseCompletionQuery(int CourseID) : IRequest<double>;