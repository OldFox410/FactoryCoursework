using Factory.Domain.Dto;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Commands.Employee;

public class DeleteEmployeeCommand : DataBaseCommand<EntityDto>
{
    protected override string _sql => "DELETE FROM Employees WHERE Id = @Id";

    public DeleteEmployeeCommand(IConfiguration config) : base(config) { }
}