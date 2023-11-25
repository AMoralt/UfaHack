using Domain;

namespace Infrastructure.Contracts;

public interface IUserProgressRepository : IRepository<UserProgress>
{
    Task<int> CreateAsync(UserProgress item, CancellationToken cancellationToken = default);
    Task<IEnumerable<UserProgress>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<UserProgress> GetByIdAsync(int progressId, CancellationToken cancellationToken = default);
    Task UpdateAsync(UserProgress item, CancellationToken cancellationToken = default);
    Task DeleteAsync(int progressId, CancellationToken cancellationToken = default);

    Task<int> GetTotalModulesCount(int courseId, CancellationToken cancellationToken = default);
    Task<int> GetCompletedModulesCount(int courseId, CancellationToken cancellationToken = default);
    Task<int> GetCompletedQuizCount(int moduleId, int userId, CancellationToken cancellationToken = default);
}