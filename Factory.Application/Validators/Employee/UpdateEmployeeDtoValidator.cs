using Factory.Domain.Dto;
using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Validators.Employee;

public class UpdateEmployeeDtoValidator : AbstractSqlValidator<EmployeeDto>
{
    public UpdateEmployeeDtoValidator(IConfiguration configuration, EmployeeDtoValidator baseValidator) : base(configuration)
    {
        Include(baseValidator);

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Не указан идентификатор записи.")
            .MustAsync(async (id, cancellationToken) => id.HasValue &&
                await CheckForExistenceAsync(id.Value, cancellationToken, "Employees"))
            .WithMessage("Не удалось найти обновляемую запись.");
    }
}