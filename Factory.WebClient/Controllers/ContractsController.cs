using Factory.Application.Commands.Contract;
using Factory.Application.Queries.Contract;
using Factory.Domain.Dto;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Factory.WebClient.Controllers;

public class ContractsController : Controller
{
    private readonly ContractQuery _contractQuery;
    private readonly ContractSingleQuery _contractSingleQuery;
    private readonly AddContractCommand _addCommand;
    private readonly UpdateContractCommand _updateCommand;
    private readonly DeleteContractCommand _deleteCommand;

    public ContractsController(
        ContractQuery contractQuery,
        ContractSingleQuery contractSingleQuery,
        AddContractCommand addCommand,
        UpdateContractCommand updateCommand,
        DeleteContractCommand deleteCommand)
    {
        _contractQuery = contractQuery;
        _contractSingleQuery = contractSingleQuery;
        _addCommand = addCommand;
        _updateCommand = updateCommand;
        _deleteCommand = deleteCommand;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? search, CancellationToken cancellationToken)
    {
        var items = await _contractQuery.ExecuteAsync(new ContractFilter(search));
        return View(items);
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var viewModel = await _contractSingleQuery.ExecuteAsync(new EntityDto(id), cancellationToken);
        if (viewModel is null)
        {
            return NotFound();
        }

        return View(viewModel);
    }
    
    [HttpGet]
    public IActionResult Create(int? employeeId = null)
    {
        var dto = new ContractDto(
            Id: 0,
            OrganizationId: 0,
            EmployeeId: employeeId ?? 0,
            ContractDate: null);

        return View(dto);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ContractDto dto, CancellationToken cancellationToken)
    {
        ValidationResult? validationResult = await _addCommand.ExecuteAsync(dto);
        if (validationResult is not null && !validationResult.IsValid)
        {
            AddValidationErrors(validationResult);
            return View(dto);
        }

        return RedirectToAction(nameof(Index), new { employeeId = dto.EmployeeId });
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ContractViewDto model, CancellationToken cancellationToken)
    {
        ValidationResult? validationResult = await _updateCommand.ExecuteAsync(model);
        if (validationResult is not null && !validationResult.IsValid)
        {
            AddValidationErrors(validationResult);
            return View(model);
        }

        return RedirectToAction(nameof(Index), new { employeeId = model.EmployeeId });
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, int employeeId, CancellationToken cancellationToken)
    {
        await _deleteCommand.ExecuteAsync(new EntityDto(id));
        return RedirectToAction(nameof(Index), new { employeeId });
    }

    private void AddValidationErrors(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
    }
}