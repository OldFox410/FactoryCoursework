using Dapper;
using FluentValidation;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Validators;

public class AbstractSqlValidator<T> : AbstractValidator<T>
{
    protected string _connectionString;
    public AbstractSqlValidator(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
                            ?? throw new InvalidOperationException("Connection string not found.");
    }

    protected async Task<bool> CheckForExistenceAsync(int id, CancellationToken cancellationToken, string tableName)
    {
        await using SqlConnection connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        
        string sql = $"""
                      SELECT CASE WHEN EXISTS (
                          SELECT 1 FROM {tableName} WHERE Id = @Id
                      ) THEN 1 ELSE 0 END;
                      """;
        
        var command = new CommandDefinition(
            sql,
            new { Id = id },
            cancellationToken: cancellationToken);
        
        var exists = await connection.QuerySingleAsync<int>(command);
        return exists == 1;
    }
}