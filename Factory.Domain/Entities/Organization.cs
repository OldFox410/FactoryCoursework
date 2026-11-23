
using Factory.Domain.Entities.Abstract;

namespace Factory.Domain.Entities;

public class Organization : Entity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? ContactNumber { get; set; }
}
