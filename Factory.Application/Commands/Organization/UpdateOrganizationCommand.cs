using Factory.Application.Validators.Organization;
using Factory.Domain.Dto;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Commands.Organization;

public class UpdateOrganizationCommand : DataBaseValidatedCommand<OrganizationDto, UpdateOrganizationDtoValidator>
{
    protected override string _sql => """
                                      UPDATE Organizations 
                                        SET Name = @Name,
                                            Address = @Address,
                                            ContactNumber = @ContactNumber
                                      WHERE Id = @Id;
                                      """;
    
    public UpdateOrganizationCommand(IConfiguration config, UpdateOrganizationDtoValidator dtoValidator) : base(config, dtoValidator) { }
}