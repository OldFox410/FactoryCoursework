namespace Factory.Domain.Dto;

public record PaymentDto(
    int? Id,
    int EmployeeId,
    decimal SalaryAmount,
    DateTime PaymentMonth,
    decimal Bonus,
    decimal Fine
);

public record PaymentViewDto(
    int? Id,
    int EmployeeId,
    string EmployeeFullName,
    decimal SalaryAmount,
    DateTime PaymentMonth,
    decimal Bonus,
    decimal Fine
    ) : PaymentDto(Id, EmployeeId, SalaryAmount, PaymentMonth, Bonus, Fine);

public record PaymentQueryDto(
    int? Id,
    string EmployeeFullName,
    decimal SalaryAmount,
    DateTime PaymentMonth,
    decimal Bonus,
    decimal Fine
);

public record PaymentFilter(DateTime? StartDate, DateTime? EndDate);