using Factory.Domain.Dto;
using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Validators.Payment;

public class UpdatePaymentDtoValidator : AbstractSqlValidator<PaymentDto>
{
    public UpdatePaymentDtoValidator(IConfiguration configuration, PaymentDtoValidator baseValidator) : base(configuration)
    {
        Include(baseValidator);

        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Не указан идентификатор записи.")
            .MustAsync(async (id, cancellationToken) => id.HasValue &&
                await CheckForExistenceAsync(id.Value, cancellationToken, "Payments"))
            .WithMessage("Не удалось найти обновляемую запись.");
    }  
}