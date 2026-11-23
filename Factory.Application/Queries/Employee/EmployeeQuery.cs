using Factory.Domain.Dto;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Queries.Employee;

public class EmployeeQuery : DataBaseQuery<EmployeeQueryDto, EmployeeSearchFilter>
{
    protected override string _queryString => @"
        SELECT e.Id,
         TRIM(CONCAT_WS(' ', e.LastName, e.FirstName, e.MiddleName)) AS FullName,
         BadgeNumber, 
         Address, 
         Phone, 
         DateOfBirth,
         d.Name AS DepartmentName
        FROM Employees e
        JOIN Departments d ON e.DepartmentId = d.Id AND (@DepartmentId IS NULL OR d.Id = @DepartmentId)
        WHERE (@Search IS NULL OR FirstName LIKE @Search OR LastName LIKE @Search OR MiddleName LIKE @Search)";

    public EmployeeQuery(IConfiguration config) : base(config) { }

    public override Task<IEnumerable<EmployeeQueryDto>> ExecuteAsync(EmployeeSearchFilter variables)
    {
        if(!string.IsNullOrEmpty(variables.Search))
            variables = variables with { Search = $"%{variables.Search}%" };
        
        return base.ExecuteAsync(variables);
    }
}