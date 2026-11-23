using Factory.Application.Validators;
using Factory.Application.Validators.Contract;
using Factory.Domain.Dto;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Commands.Contract;

public class AddContractCommand : DataBaseValidatedCommand<ContractDto, ContractDtoValidator>
{
    protected override string _sql => """
                                      INSERT INTO Contracts (OrganizationId, EmployeeId, ContractDate) 
                                      VALUES (@OrganizationId, @EmployeeId, @ContractDate)
                                      """;
    
    public AddContractCommand(IConfiguration config, ContractDtoValidator validator) : base(config, validator) { }
}