using Dapper;
using Domain;
using Infrastructure.Contracts;
using Npgsql;

namespace Infrastructure.Repository;

public class UserSubmissionRepository : IUserSubmissionRepository
{
    private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;

    public UserSubmissionRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }
    
    public async Task<int> CreateAsync(UserSubmission submission, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            INSERT INTO UserSubmissions (UserID, QuizID, AnswerGiven, IsCorrect)
            VALUES (@UserID, @QuizID, @AnswerGiven, @IsCorrect)
            RETURNING SubmissionID;
        ";

        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);

        var submissionId = await connection.ExecuteScalarAsync<int>(sql, new
        {
            submission.UserID,
            submission.QuizID,
            submission.AnswerGiven,
            submission.IsCorrect
        });

        return submissionId;
    }

}