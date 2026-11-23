namespace Factory.Domain.Dto;

public record OrganizationDto(
    int Id,
    string Name,
    string? Address,
    string? ContactNumber
);