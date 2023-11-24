using Dapper;
using Domain.AggregationModels;
using Npgsql;
using O2GEN.Models;

public class CoursesRepository : ICoursesRepository
{
    public CoursesRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }
    private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;


    public async Task<int> CreateAsync(Courses itemToCreate, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            INSERT INTO Courses (Title, Description, Subject, PhotoData, CreatedBy)
            VALUES 
                (@title, @description, @subject, @photodata, @createdby)
            RETURNING courseid;
            ";
        var parameters = new
        {
            title = itemToCreate.Title,
            description = itemToCreate.Description,
            subject = itemToCreate.Subject,
            photodata = itemToCreate.PhotoData,
            createdby = itemToCreate.CreatedBy
        };
        
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        var Id = await connection.ExecuteScalarAsync(sql, param: parameters);

        return (int)Id;
    }

    public async Task<IEnumerable<Courses>> GetAllAsync(int limit, int offset, CancellationToken cancellationToken = default)
    {
        string sql = @"
            SELECT CourseID AS Id, Title, Description, Subject, PhotoData, CreatedBy, CreationDate, UpdatedDate
            FROM Courses
            OFFSET @Offset";

        if (limit != 0) //if limit is entered by user
            sql += "\rLIMIT @Limit";
        
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        var result =  await connection.QueryAsync<Courses>(sql, param: new {Offset = offset, Limit = limit});
        
        return result;
    }

    public Task<Courses> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Courses itemToUpdate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(Courses itemToDelete, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}