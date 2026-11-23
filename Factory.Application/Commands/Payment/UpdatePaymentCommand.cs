using Factory.Application.Validators;
using Factory.Application.Validators.Contract;
using Factory.Application.Validators.Payment;
using Factory.Domain.Dto;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Commands.Payment;

public class UpdatePaymentCommand : DataBaseValidatedCommand<PaymentDto, PaymentDtoValidator>
{
    protected override string _sql => """
                                      UPDATE Payments
                                      SET EmployeeId = @EmployeeId, 
                                          SalaryAmount = @SalaryAmount, 
                                          PaymentMonth = @PaymentMonth, 
                                          Bonus = @Bonus, Fine = @Fine 
                                      WHERE Id = @Id
                                      """;
    
    public UpdatePaymentCommand(IConfiguration config, PaymentDtoValidator validator) : base(config, validator) { }
}