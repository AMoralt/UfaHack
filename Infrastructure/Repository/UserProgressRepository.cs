using Dapper;
using Domain;
using Infrastructure.Contracts;
using Npgsql;

namespace Infrastructure.Repository;

public class UserProgressRepository : IUserProgressRepository
{
    // Assuming you have a database connection factory like IDbConnectionFactory

    private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
    private readonly IQuizRepository _quizRepository;

    public UserProgressRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IQuizRepository quizRepository)
    {
        _dbConnectionFactory = dbConnectionFactory;
        _quizRepository = quizRepository;
    }

    public async Task<int> CreateAsync(UserProgress item, CancellationToken cancellationToken = default)
    {
        const string sql = @"
        INSERT INTO UserProgress (UserID, CourseID, ModuleID, CompletionStatus, Score)
        VALUES (@UserID, @CourseID, @ModuleID, @CompletionStatus, @Score)
        RETURNING ProgressID;
    ";

        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        var newId = await connection.ExecuteScalarAsync<int>(sql, item);

        return newId;
    }

    public Task<IEnumerable<UserProgress>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<UserProgress> GetByIdAsync(int progressId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(UserProgress item, CancellationToken cancellationToken = default)
    {
        const string sql = @"
        UPDATE UserProgress
        SET 
            UserID = @UserID,
            CourseID = @CourseID,
            ModuleID = @ModuleID,
            CompletionStatus = @CompletionStatus,
            Score = @Score
        WHERE Id = @ProgressID;
    ";
        
        const string sql2 = @"
        SELECT COUNT(*)
        FROM UserSubmissions us
        JOIN Quizzes q ON us.QuizID = q.QuizID
        WHERE us.UserID = @UserID
        AND q.ModuleID = @ModuleID
        AND us.IsCorrect = TRUE;
    ";
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        var correctAnswersCount = await connection.ExecuteScalarAsync<int>(sql2, new { UserID = item.UserID, ModuleID = item.ModuleID });

        var totalQuizzes = await _quizRepository.GetAllAsync(item.ModuleID, cancellationToken);
        var totalQuizCount = totalQuizzes.Count();

        if (totalQuizCount == 0)
            throw new Exception();

        var parameters = new
        {
            ProgressID = item.Id,
            UserID = item.UserID,
            CourseID = item.CourseID,
            ModuleID = item.ModuleID,
            CompletionStatus = item.CompletionStatus,
            Score = GetGradeFromPercentage((decimal)correctAnswersCount / totalQuizCount)
        };

        await connection.ExecuteAsync(sql, parameters);
    }
    
    public int GetGradeFromPercentage(decimal correctPercentage)
    {
        if (correctPercentage >= 0.90m) // 90% и выше
            return 5;
        else if (correctPercentage >= 0.70m) // от 70% до 89%
            return 4;
        else if (correctPercentage >= 0.50m) // от 50% до 69%
            return 3;
        // Можно добавить дополнительные условия для других оценок
        else
            return 2; // или другая минимальная оценка
    }

    public Task DeleteAsync(int progressId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetTotalModulesCount(int courseId, CancellationToken cancellationToken = default)
    {
        const string sql = @"
        SELECT COUNT(*)
        FROM Modules
        WHERE CourseID = @CourseID;
    ";

        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        var totalModulesCount = await connection.ExecuteScalarAsync<int>(sql, new { CourseID = courseId });

        return totalModulesCount;
    }

    public async Task<int> GetCompletedModulesCount(int courseId, CancellationToken cancellationToken = default)
    {
        const string sql = @"
        SELECT COUNT(*)
        FROM UserProgress
        WHERE CourseID = @CourseID AND CompletionStatus = TRUE;
    ";

        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        var completedModulesCount = await connection.ExecuteScalarAsync<int>(sql, new { CourseID = courseId });

        return completedModulesCount;
    }
}