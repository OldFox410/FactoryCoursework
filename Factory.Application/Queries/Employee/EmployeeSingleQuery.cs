using Factory.Domain.Dto;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Queries.Employee;

public class EmployeeSingleQuery : DataBaseSingleQuery<EmployeeViewDto>
{
    protected override string _sql => @"SELECT 
                                                e.Id,
                                                e.FirstName,
                                                e.MiddleName,
                                                e.LastName,
                                                e.BadgeNumber, 
                                                e.Address,
                                                e.Phone, 
                                                e.DateOfBirth,
                                                e.DepartmentId,
                                                d.Name AS DepartmentName
                                            FROM Employees e
                                            JOIN Departments d ON e.DepartmentId = d.Id
                                            WHERE e.Id = @Id";
    
    public EmployeeSingleQuery(IConfiguration configuration) : base(configuration)
    {
    }
}