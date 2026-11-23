
using Factory.Domain.Entities.Abstract;

namespace Factory.Domain.Entities;

public class Payment : Entity
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public decimal SalaryAmount { get; set; }
    public DateTime PaymentMonth { get; set; }
    public decimal Bonus { get; set; }
    public decimal Fine { get; set; }
}
