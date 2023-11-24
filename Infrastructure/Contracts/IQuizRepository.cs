
using Domain.AggregationModels;
using O2GEN.Models;

public interface IQuizRepository : IRepository<Quiz>
{
    Task<int> CreateAsync(Quiz itemToCreate, CancellationToken cancellationToken = default);
    Task<IEnumerable<Quiz>> GetAllAsync(int moduleId, CancellationToken cancellationToken = default);
    Task<Quiz> GetAsync(int quizId, CancellationToken cancellationToken = default);
    Task UpdateAsync(Quiz itemToUpdate, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(Quiz itemToDelete, CancellationToken cancellationToken = default);
}