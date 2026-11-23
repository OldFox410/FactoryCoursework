using Factory.Domain.Dto;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Queries.Department;

public class DepartmentQuery : DataBaseQuery<DepartmentDto, EntityDto>
{
    protected override string _queryString => @"
        SELECT
            Id,
            Name,
            Code
        FROM Departments";

    public DepartmentQuery(IConfiguration config) : base(config) { }
}