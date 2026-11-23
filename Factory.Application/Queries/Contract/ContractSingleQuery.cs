using Factory.Domain.Dto;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Queries.Contract;

public class ContractSingleQuery : DataBaseSingleQuery<ContractViewDto>
{
    public ContractSingleQuery(IConfiguration configuration) : base(configuration) {}

    protected override string _sql => """
                                      SELECT
                                          c.Id,
                                          c.OrganizationId,
                                          o.Name AS OrganizationName,
                                          c.EmployeeId,
                                          TRIM(CONCAT_WS(' ', e.LastName, e.FirstName, e.MiddleName)) AS EmployeeFullName,
                                          c.ContractDate
                                      FROM Contracts c
                                      JOIN Organizations o ON o.Id = c.OrganizationId
                                      JOIN Employees e ON e.Id = c.EmployeeId
                                      WHERE c.Id = @Id
                                      """;
}