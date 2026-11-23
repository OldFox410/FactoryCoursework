using Factory.Application.Commands.Payment;
using Factory.Application.Queries.Payment;
using Factory.Domain.Dto;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Factory.WebClient.Controllers;

public class PaymentsController : Controller
{
    private readonly PaymentQuery _paymentQuery;
    private readonly PaymentSingleQuery _paymentSingleQuery;
    private readonly AddPaymentCommand _addCommand;
    private readonly UpdatePaymentCommand _updateCommand;
    private readonly DeletePaymentCommand _deleteCommand;

    public PaymentsController(
        PaymentQuery paymentQuery,
        PaymentSingleQuery paymentSingleQuery,
        AddPaymentCommand addCommand,
        UpdatePaymentCommand updateCommand,
        DeletePaymentCommand deleteCommand)
    {
        _paymentQuery = paymentQuery;
        _paymentSingleQuery = paymentSingleQuery;
        _addCommand = addCommand;
        _updateCommand = updateCommand;
        _deleteCommand = deleteCommand;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index(PaymentFilter filter, CancellationToken cancellationToken)
    {
        filter ??= new PaymentFilter(null, null);
        var items = await _paymentQuery.ExecuteAsync(filter);
        return View(items);
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var viewModel = await _paymentSingleQuery.ExecuteAsync(new EntityDto(id), cancellationToken);
        if (viewModel is null)
        {
            return NotFound();
        }

        return View(viewModel);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        var dto = new PaymentDto(
            Id: 0,
            EmployeeId: 0,
            SalaryAmount: 0,
            PaymentMonth: DateTime.UtcNow,
            Bonus: 0,
            Fine: 0);
        return View(dto);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PaymentDto dto, CancellationToken cancellationToken)
    {
        ValidationResult? validationResult = await _addCommand.ExecuteAsync(dto);
        if (validationResult is not null && !validationResult.IsValid)
        {
            AddValidationErrors(validationResult);
            return View(dto);
        }

        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(PaymentViewDto model, CancellationToken cancellationToken)
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