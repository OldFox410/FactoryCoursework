using Factory.Application.Validators;
using Factory.Application.Validators.Contract;
using Factory.Domain.Dto;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Commands.Contract;

public class UpdateContractCommand : DataBaseValidatedCommand<ContractDto, ContractDtoValidator>
{
    protected override string _sql => """
                                      UPDATE Contracts 
                                      SET OrganizationId = @OrganizationId, 
                                          EmployeeId = @EmployeeId, 
                                          ContractDate = @ContractDate 
                                      WHERE Id = @Id
                                      """;
    
    public UpdateContractCommand(IConfiguration config, ContractDtoValidator validator) : base(config, validator) { }
}