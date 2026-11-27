using Factory.Domain.Dto;
using FluentValidation;

namespace Factory.Application.Validators.Organization;

public class OrganizationDtoValidator : AbstractValidator<OrganizationDto>
{
    public OrganizationDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Название организации обязательно");
        RuleFor(x => x.Name).MaximumLength(200).WithMessage("Название организации не должно превышать 200 символов");
        RuleFor(x => x.ContactNumber)
            .MaximumLength(50).WithMessage("Контактный номер не должен превышать 50 символов");
    }
}