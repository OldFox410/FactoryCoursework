using Factory.Domain.Dto;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Commands.Payment;

public class DeletePaymentCommand : DataBaseCommand<EntityDto>
{
    protected override string _sql => "DELETE FROM Payments WHERE Id = @Id";

    public DeletePaymentCommand(IConfiguration config) : base(config) { }
}