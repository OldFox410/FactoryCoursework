using Factory.Domain.Dto;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Queries.Payment;

public class PaymentSingleQuery : DataBaseSingleQuery<PaymentViewDto>
{
    protected override string _sql => @"
        SELECT p.Id,
         p.EmployeeId,
         TRIM(CONCAT_WS(' ', e.LastName, e.FirstName, e.MiddleName)) AS EmployeeFullName, 
         p.SalaryAmount,
         p.PaymentMonth,
         p.Bonus,
         p.Fine 
        FROM Payments p
        JOIN Employees e ON e.Id = p.EmployeeId
        WHERE p.Id = @Id";
    
    public PaymentSingleQuery(IConfiguration configuration) : base(configuration) {}
}