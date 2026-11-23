using Dapper;
using Factory.Domain.Dto;
using FluentValidation;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Validators.Contract;

public class ContractDtoValidator : AbstractSqlValidator<ContractDto>
{
    public ContractDtoValidator(IConfiguration config) : base(config)
    {
        RuleFor(x => x.EmployeeId).NotEmpty().WithMessage("Указание сотрудника обязательно.");
        RuleFor(x => x.OrganizationId).NotEmpty().WithMessage("Указание организации обязательно.");
        RuleFor(x => x.EmployeeId)
            .MustAsync(async (employeeId, cancellationToken) =>
            {
                await using SqlConnection connection = new SqlConnection(_connectionString);
                await connection.OpenAsync(cancellationToken);
                
                const string sql = """
                                   SELECT CASE WHEN EXISTS (
                                       SELECT 1 FROM Employees e 
                                       JOIN Departments d ON e.DepartmentId = d.Id AND d.Code = 'ACC'
                                       WHERE e.Id = @EmployeeId
                                   ) THEN 1 ELSE 0 END;
                                   """;
            
                var command = new CommandDefinition(
                    sql,
                    new { EmployeeId = employeeId },
                    cancellationToken: cancellationToken);
            
                var exists = await connection.QuerySingleAsync<int>(command);
                return exists == 1;
            })
            .WithMessage("Указанного бухгалтера не существует.");
        RuleFor(x => x.OrganizationId)
            .MustAsync(async (organizationId, cancellationToken) => organizationId.HasValue && 
                await CheckForExistenceAsync(organizationId.Value, cancellationToken, "Organizations"))
            .WithMessage("Указанной организации не существует.");
    }
}