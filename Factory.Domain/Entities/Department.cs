
using Factory.Domain.Entities.Abstract;

namespace Factory.Domain.Entities;

public class Department : Entity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}
