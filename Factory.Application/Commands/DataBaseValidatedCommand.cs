using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Commands;

public abstract class DataBaseValidatedCommand<TDto, TValidator> 
    : DataBaseCommand<TDto> 
    where TValidator : AbstractValidator<TDto>
{
    protected readonly TValidator _validator;

    protected DataBaseValidatedCommand(IConfiguration config, TValidator validator) : base(config)
    {
        _validator = validator;
    }

    public override async Task<ValidationResult?> ExecuteAsync(TDto parameters)
    {
        var result = await _validator.ValidateAsync(parameters);
        if (!result.IsValid)
            return result;
        await base.ExecuteAsync(parameters);
        return null;
    }
}