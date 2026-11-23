using Factory.Domain.Dto;
using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Validators.Organization;

public class UpdateOrganizationDtoValidator : AbstractSqlValidator<OrganizationDto>
{
    public UpdateOrganizationDtoValidator(IConfiguration configuration, OrganizationDtoValidator baseValidator) : base(configuration)
    {
        Include(baseValidator);

        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Не указан идентификатор записи.")
            .MustAsync(async (id, cancellationToken) =>
                await CheckForExistenceAsync(id, cancellationToken, "Organizations"))
            .WithMessage("Не удалось найти обновляемую запись.");
    }
}