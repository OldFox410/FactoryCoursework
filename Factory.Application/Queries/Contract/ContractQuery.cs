using Factory.Domain.Dto;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Queries.Contract;

public class ContractQuery : DataBaseQuery<ContractQueryDto, ContractFilter>
{
    protected override string _queryString => @"
        SELECT
            c.Id,
            o.Name AS OrganizationName,
            TRIM(CONCAT_WS(' ', e.LastName, e.FirstName, e.MiddleName)) AS EmployeeFullName,
            c.ContractDate
        FROM Contracts c
        JOIN Organizations o ON o.Id = c.OrganizationId
        JOIN Employees e ON e.Id = c.EmployeeId 
             AND (@EmployeeId IS NULL OR c.EmployeeId = @EmployeeId);";

    public ContractQuery(IConfiguration config) : base(config) { }
}