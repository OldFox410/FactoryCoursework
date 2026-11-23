using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Commands;

public abstract class DataBaseCommand<TDto>
{
    protected readonly string _connectionString;
    protected abstract string _sql { get; }

    public DataBaseCommand(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
                            ?? throw new InvalidOperationException("Connection string not found.");
    }

    public virtual async Task ExecuteAsync(TDto parameters)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.ExecuteAsync(_sql, parameters);
        return;
    }
}