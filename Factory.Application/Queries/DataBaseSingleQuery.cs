using Dapper;
using Factory.Domain.Dto;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Queries;

public abstract class DataBaseSingleQuery<TResult>
{
    protected readonly string _connectionString;

    protected DataBaseSingleQuery(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
                            ?? throw new InvalidOperationException("Connection string not found.");
    }

    protected abstract string _sql { get; }

    public virtual async Task<TResult?> ExecuteAsync(
        EntityDto dto,
        CancellationToken cancellationToken = default)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var command = new CommandDefinition(
            _sql,
            dto,
            cancellationToken: cancellationToken);

        return await connection.QuerySingleOrDefaultAsync<TResult>(command);
    }
}