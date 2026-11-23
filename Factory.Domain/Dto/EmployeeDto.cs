
using Factory.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Factory.Domain.Dto;

public record EmployeeDto(
    int? Id, 
    string FirstName, 
    string? MiddleName, 
    string LastName, 
    string BadgeNumber, 
    string? Address, 
    string? Phone, 
    DateTime? DateOfBirth,
    int DepartmentId
);

public record EmployeeViewDto(
    int? Id, 
    string FirstName, 
    string? MiddleName, 
    string LastName,
    string BadgeNumber, 
    string? Address, 
    string? Phone, 
    DateTime? DateOfBirth,
    int DepartmentId,
    string DepartmentName
) : EmployeeDto(Id, FirstName, MiddleName, LastName, BadgeNumber, Address, Phone, DateOfBirth, DepartmentId);

public record EmployeeQueryDto(
    int? Id, 
    string FullName, 
    string BadgeNumber, 
    string? Address, 
    string? Phone,
    DateTime? DateOfBirth,
    string DepartmentName
);

public record EmployeeSearchFilter(string? Search, int? DepartmentId);