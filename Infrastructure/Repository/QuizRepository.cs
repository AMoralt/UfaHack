using Dapper;
using Npgsql;
using O2GEN.Models;

namespace Infrastructure.Repository;

public class QuizRepository : IQuizRepository
{
    private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;

    public QuizRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<int> CreateAsync(Quiz itemToCreate, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            INSERT INTO quizzes (moduleid, question, options, correctoption, explanation)
            VALUES 
                (@moduleid, @question, @options, @correctoption, @explanation)
            RETURNING quizid;
            ";
        var parameters = new
        {
            moduleid = itemToCreate.ModuleID,
            question = itemToCreate.Question,
            options = itemToCreate.Options,
            correctoption = itemToCreate.CorrectOption,
            explanation = itemToCreate.Explanation
        };

        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        var Id = await connection.ExecuteScalarAsync(sql, param: parameters);

        return (int)Id;
    }

    public async Task<IEnumerable<Quiz>> GetAllAsync(int moduleId, CancellationToken cancellationToken = default)
    {
        string sql = @"
            SELECT quizid AS Id, moduleid, question, options, correctoption, explanation
            FROM quizzes
            WHERE moduleid = @moduleid";
        
        var parameters = new
        {
            moduleid = moduleId
        };

        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        var result = await connection.QueryAsync<Quiz>(sql, param: parameters);
        
        return result;
    }

    public Task<Quiz> GetAsync(int quizId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Quiz itemToUpdate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(Quiz itemToDelete, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}