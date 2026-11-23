using Factory.Application.Validators;
using Factory.Application.Validators.Contract;
using Factory.Application.Validators.Payment;
using Factory.Domain.Dto;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Commands.Payment;

public class AddPaymentCommand : DataBaseValidatedCommand<PaymentDto, PaymentDtoValidator>
{
    protected override string _sql => """
                                      INSERT INTO Payments (EmployeeId, SalaryAmount, PaymentMonth, Bonus, Fine)
                                      VALUES (@EmployeeId, @SalaryAmount, @PaymentMonth, @Bonus, @Fine);
                                      """;
    
    public AddPaymentCommand(IConfiguration config, PaymentDtoValidator dtoValidator) : base(config, dtoValidator) { }
}