using Factory.Application.Validators.Contract;
using Factory.Application.Validators.Organization;
using Factory.Domain.Dto;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Commands.Organization;

public class AddOrganizationCommand : DataBaseValidatedCommand<OrganizationDto, OrganizationDtoValidator>
{
    protected override string _sql => """
                                      INSERT INTO Organizations (Name, Address, ContactNumber)
                                      VALUES (@Name, @Address, @ContactNumber);
                                      """;
    
    public AddOrganizationCommand(IConfiguration config, OrganizationDtoValidator dtoValidator) : base(config, dtoValidator) { }
}