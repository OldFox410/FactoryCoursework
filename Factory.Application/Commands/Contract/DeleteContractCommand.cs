using Factory.Domain.Dto;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Commands.Contract;

public class DeleteContractCommand : DataBaseCommand<EntityDto>
{
    protected override string _sql => "DELETE FROM Contracts WHERE Id = @Id";

    public DeleteContractCommand(IConfiguration config) : base(config) { }
}