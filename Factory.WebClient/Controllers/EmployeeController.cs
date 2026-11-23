using Factory.Application.Commands.Employee;
using Factory.Application.Queries.Department;
using Factory.Application.Queries.Employee;
using Factory.Domain.Dto;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Factory.WebClient.Controllers;

public class EmployeesController : Controller
{
    private readonly EmployeeQuery _employeeQuery;
    private readonly EmployeeSingleQuery _employeeSingleQuery;
    private readonly AddEmployeeCommand _addCommand;
    private readonly UpdateEmployeeCommand _updateCommand;
    private readonly DeleteEmployeeCommand _deleteCommand;
    private readonly DepartmentQuery _departmentQuery;

    public EmployeesController(
        EmployeeQuery employeeQuery,
        EmployeeSingleQuery employeeSingleQuery,
        AddEmployeeCommand addCommand,
        UpdateEmployeeCommand updateCommand,
        DeleteEmployeeCommand deleteCommand,
        DepartmentQuery departmentQuery)
    {
        _employeeQuery = employeeQuery;
        _employeeSingleQuery = employeeSingleQuery;
        _addCommand = addCommand;
        _updateCommand = updateCommand;
        _deleteCommand = deleteCommand;
        _departmentQuery = departmentQuery;
    }

    [HttpGet]
    public async Task<IActionResult> Index(EmployeeSearchFilter filter, CancellationToken cancellationToken)
    {
        filter ??= new EmployeeSearchFilter(null, null);
        var items = await _employeeQuery.ExecuteAsync(filter);
        await PopulateDepartmentsAsync();
        return View(items);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var viewModel = await _employeeSingleQuery.ExecuteAsync(new EntityDto(id), cancellationToken);
        if (viewModel is null)
        {
            return NotFound();
        }

        await PopulateDepartmentsAsync();
        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var dto = new EmployeeDto(
            Id: 0,
            FirstName: string.Empty,
            MiddleName: null,
            LastName: string.Empty,
            BadgeNumber: string.Empty,
            Address: null,
            Phone: null,
            DateOfBirth: null,
            DepartmentId: 0);
        
        await PopulateDepartmentsAsync();
        return View(dto);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EmployeeDto dto, CancellationToken cancellationToken)
    {
        ValidationResult? validationResult = await _addCommand.ExecuteAsync(dto);
        if (validationResult is not null && !validationResult.IsValid)
        {
            AddValidationErrors(validationResult);
            await PopulateDepartmentsAsync();
            return View(dto);
        }

        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EmployeeViewDto model, CancellationToken cancellationToken)
    {
        ValidationResult? validationResult = await _updateCommand.ExecuteAsync(model);
        if (validationResult is not null && !validationResult.IsValid)
        {
            AddValidationErrors(validationResult);
            await PopulateDepartmentsAsync();
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _deleteCommand.ExecuteAsync(new EntityDto(id));
        return RedirectToAction(nameof(Index));
    }

    private void AddValidationErrors(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
    }
    
    private async Task PopulateDepartmentsAsync()
    {
        var departments = await _departmentQuery.ExecuteAsync(new(null));
        ViewBag.Departments = departments;
    }
}