using Factory.Domain.Dto;
using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Validators.Payment;

public class PaymentDtoValidator : AbstractSqlValidator<PaymentDto>
{
    public PaymentDtoValidator(IConfiguration config) : base(config)
    {
        RuleFor(x => x.SalaryAmount).NotEmpty().WithMessage("Сумма зарплаты обязательна");
        RuleFor(x => x.PaymentMonth).NotEmpty().WithMessage("Месяц выплаты обязателен");
        RuleFor(x => x.Bonus).LessThan(0).WithMessage("Премия не может быть отрицательной");
        RuleFor(x => x.Fine).LessThan(0).WithMessage("Штраф не может быть отрицательным");
        RuleFor(x => x.EmployeeId).GreaterThan(0).WithMessage("ID сотрудника обязателен")
            .MustAsync(async (employeeId, cancellationToken) =>
                await CheckForExistenceAsync(employeeId, cancellationToken, "Employees"))
            .WithMessage("Указанного сотрудника не существует.");
    }
}