using Factory.Application.Validators;
using Factory.Application.Validators.Employee;
using Factory.Domain.Dto;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Commands.Employee;

public class UpdateEmployeeCommand : DataBaseValidatedCommand<EmployeeDto, EmployeeDtoValidator>
{
    protected override string _sql => @"
        UPDATE Employees 
        SET FirstName = @FirstName, 
            MiddleName = @MiddleName, 
            LastName = @LastName, 
            BadgeNumber = @BadgeNumber, 
            Address = @Address, 
            Phone = @Phone, 
            DateOfBirth = @DateOfBirth, 
            DepartmentId = @DepartmentId
        WHERE Id = @Id";

    public UpdateEmployeeCommand(IConfiguration config, EmployeeDtoValidator validator) : base(config, validator) { }
}