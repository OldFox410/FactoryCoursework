namespace Factory.Domain.Dto;

public record ContractDto(
    int? Id,
    int? OrganizationId,
    int? EmployeeId,
    DateTime? ContractDate
);

public record ContractViewDto(
    int? Id,
    int? OrganizationId,
    string OrganizationName,
    int? EmployeeId,
    string EmployeeFullName,
    DateTime? ContractDate) : ContractDto(Id, OrganizationId, EmployeeId, ContractDate);

public record ContractQueryDto(
    int Id,
    string OrganizationName, 
    string EmployeeFullName,
    DateTime? ContractDate
);

public record ContractFilter(int? EmployeeId);