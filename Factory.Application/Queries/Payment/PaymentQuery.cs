using Factory.Domain.Dto;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Queries.Payment;

public class PaymentQuery : DataBaseQuery<PaymentQueryDto, PaymentFilter>
{
    protected override string _queryString => @"
        SELECT p.Id,
         TRIM(CONCAT_WS(' ', e.LastName, e.FirstName, e.MiddleName)) AS EmployeeFullName, 
         p.SalaryAmount,
         p.PaymentMonth,
         p.Bonus,
         p.Fine 
        FROM Payments p
        JOIN Employees e ON e.Id = p.EmployeeId
        WHERE (@StartDate IS NULL OR PaymentMonth >= @StartDate) AND (@EndDate IS NULL OR PaymentMonth <= @EndDate)";

    public PaymentQuery(IConfiguration config) : base(config)
    {
    }
}