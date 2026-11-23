
namespace Factory.Domain.Entities;

public class Contract
{
    public int Id { get; set; }
    public int OrganizationId { get; set; }
    public int EmployeeId { get; set; }
    public DateTime? ContractDate { get; set; }
}
