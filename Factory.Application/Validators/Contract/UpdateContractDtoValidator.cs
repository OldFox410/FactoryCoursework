using Factory.Domain.Dto;
using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Validators.Contract;

public class UpdateContractDtoValidator : AbstractSqlValidator<ContractDto>
{
    public UpdateContractDtoValidator(IConfiguration configuration, ContractDtoValidator baseValidator) : base(configuration)
    {
        Include(baseValidator);

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Не указан идентификатор записи.")
            .MustAsync(async (id, cancellationToken) => id.HasValue &&
                await CheckForExistenceAsync(id.Value, cancellationToken, "Contracts"))
            .WithMessage("Не удалось найти обновляемую запись.");
    }
}