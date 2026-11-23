using Factory.Application.Commands.Organization;
using Factory.Application.Queries.Organization;
using Factory.Domain.Dto;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Factory.WebClient.Controllers;

public class OrganizationsController : Controller
{
    private readonly OrganizationQuery _organizationQuery;
    private readonly AddOrganizationCommand _addCommand;
    private readonly UpdateOrganizationCommand _updateCommand;
    private readonly DeleteOrganizationCommand _deleteCommand;

    public OrganizationsController(
        OrganizationQuery organizationQuery,
        AddOrganizationCommand addCommand,
        UpdateOrganizationCommand updateCommand,
        DeleteOrganizationCommand deleteCommand)
    {
        _organizationQuery = organizationQuery;
        _addCommand = addCommand;
        _updateCommand = updateCommand;
        _deleteCommand = deleteCommand;
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var items = await _organizationQuery.ExecuteAsync(new EntityDto(null));
        return View(items);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var dto = new OrganizationDto(
            Id: 0,
            Name: string.Empty,
            Address: null,
            ContactNumber: null);

        return View(dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(OrganizationDto dto, CancellationToken cancellationToken)
    {
        ValidationResult? validationResult = await _addCommand.ExecuteAsync(dto);
        if (validationResult is not null && !validationResult.IsValid)
        {
            AddValidationErrors(validationResult);
            return View(dto);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var items = await _organizationQuery.ExecuteAsync(new EntityDto(id));
        var dto = items?.FirstOrDefault();
        if (dto is null)
        {
            return NotFound();
        }

        return View(dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(OrganizationDto model, CancellationToken cancellationToken)
    {
        ValidationResult? validationResult = await _updateCommand.ExecuteAsync(model);
        if (validationResult is not null && !validationResult.IsValid)
        {
            AddValidationErrors(validationResult);
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
}