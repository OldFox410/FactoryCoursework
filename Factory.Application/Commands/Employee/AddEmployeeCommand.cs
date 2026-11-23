using Factory.Application.Validators;
using Factory.Application.Validators.Employee;
using Factory.Domain.Dto;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Commands.Employee;

public class AddEmployeeCommand : DataBaseValidatedCommand<EmployeeDto, EmployeeDtoValidator>
{
    protected override string _sql => @"
        INSERT INTO Employees (FirstName, MiddleName, LastName, BadgeNumber, Address, Phone, DateOfBirth, DepartmentId)
        VALUES (@FirstName, @MiddleName, @LastName, @BadgeNumber, @Address, @Phone, @DateOfBirth, @DepartmentId)";

    public AddEmployeeCommand(IConfiguration config, EmployeeDtoValidator validator) : base(config, validator) { }
}