using Factory.Domain.Dto;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Commands.Organization;

public class DeleteOrganizationCommand : DataBaseCommand<EntityDto>
{
    protected override string _sql => "DELETE FROM Organizations WHERE Id = @Id";
    
    public DeleteOrganizationCommand(IConfiguration config) : base(config) { }
}