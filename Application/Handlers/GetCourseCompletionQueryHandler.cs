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
        // Получение общего количества модулей в курсе
        var totalModules = await _userProgressRepository.GetTotalModulesCount(request.CourseID, cancellationToken);

        // Получение количества завершенных модулей
        var completedModules = await _userProgressRepository.GetCompletedModulesCount(request.CourseID, cancellationToken);

        if (totalModules == 0)
        {
            return 0; // Избегайте деления на ноль
        }

        // Вычисление процента завершенности
        var completionPercentage = (double)completedModules / totalModules * 100;
        return completionPercentage;
    }
}

public record GetCourseCompletionQuery(int CourseID) : IRequest<double>;