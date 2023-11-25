using Domain;
using Infrastructure.Contracts;
using MediatR;

namespace Application.Commands;

public class UpdateUserProgressCommandHandler : IRequestHandler<UpdateUserProgressCommand, bool>
{
    private readonly IUserProgressRepository _userProgressRepository;

    public UpdateUserProgressCommandHandler(IUserProgressRepository userProgressRepository)
    {
        _userProgressRepository = userProgressRepository;
    }

    public async Task<bool> Handle(UpdateUserProgressCommand request, CancellationToken cancellationToken)
    {
        var userProgress = new UserProgress
        {
            UserID = request.UserID,
            CourseID = request.CourseID,
            ModuleID = request.ModuleID,
            CompletionStatus = request.CompletionStatus,
        };

        await _userProgressRepository.UpdateAsync(userProgress, cancellationToken);
        return true;
    }
}

public record UpdateUserProgressCommand(
    int UserID, int CourseID, int ModuleID, bool CompletionStatus) : IRequest<bool>;