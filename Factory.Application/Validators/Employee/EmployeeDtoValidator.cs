using Factory.Domain.Dto;
using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Validators.Employee;

public class EmployeeDtoValidator : AbstractSqlValidator<EmployeeDto>
{
    public EmployeeDtoValidator(IConfiguration config) : base(config)
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("Имя обязательно для заполнения")
            .MaximumLength(50).WithMessage("Имя не должно превышать 50 символов");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Фамилия обязательна для заполнения")
            .MaximumLength(50).WithMessage("Фамилия не должна превышать 50 символов");
        RuleFor(x => x.MiddleName).MaximumLength(50).WithMessage("Отчество не должно превышать 50 символов");
        RuleFor(x => x.BadgeNumber).NotEmpty().WithMessage("Номер жетона обязателен")
            .MaximumLength(20).WithMessage("Номер жетона не должен превышать 20 символов");
        RuleFor(x => x.Phone)
            .Matches(@"^\+?[1-9]\d{9,14}$").WithMessage("Номер телефона некорректен");
        RuleFor(x => x.DepartmentId)
            .GreaterThan(0).WithMessage("Отдел обязателен для заполнения.")
            .MustAsync(async (departmentId, cancellationToken) => await CheckForExistenceAsync(departmentId, cancellationToken, "Departments"))
            .WithMessage("Указанного отдела не существует.");
    }
}