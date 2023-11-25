using Domain;

namespace Infrastructure.Contracts;

public interface IUserSubmissionRepository
{
    Task<int> CreateAsync(UserSubmission submission, CancellationToken cancellationToken = default);
}