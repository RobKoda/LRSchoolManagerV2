using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.Persistence;
using LRSchoolV2.Domain.CustomerInvoices;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.CancelCustomerInvoice;

public class CancelCustomerInvoiceRequestValidation : AbstractValidator<CancelCustomerInvoiceRequest>
{
    private readonly ICustomerInvoicesRepository _customerInvoicesRepository;

    public CancelCustomerInvoiceRequestValidation(ICustomerInvoicesRepository inCustomerInvoicesRepository)
    {
        _customerInvoicesRepository = inCustomerInvoicesRepository;
    }

    public override async Task<ValidationResult> ValidateAsync(ValidationContext<CancelCustomerInvoiceRequest> inContext,
        CancellationToken inCancellation = new())
    {
        var customerInvoices = await _customerInvoicesRepository.GetCustomerInvoicesAsync();
        var lastCustomerInvoice = customerInvoices.MaxBy(inInvoice => inInvoice.Date);
        ValidateIsLastCustomerInvoice(lastCustomerInvoice!);
        ValidateCanBeDeleted();
        
        return await base.ValidateAsync(inContext, inCancellation);
    }

    private void ValidateIsLastCustomerInvoice(CustomerInvoice inLastCustomerInvoice) =>
        RuleFor(inRequest => inRequest.CustomerInvoice)
            .Must(inInvoice => inInvoice.Id == inLastCustomerInvoice.Id)
            .WithMessage(CancelCustomerInvoiceRequestValidationErrors.NotTheLastCustomerInvoice);
    
    private void ValidateCanBeDeleted() =>
        RuleFor(inRequest => inRequest.CustomerInvoice.Id)
            .MustAsync((inInvoiceId, _) => _customerInvoicesRepository.CanCustomerInvoiceBeDeletedAsync(inInvoiceId))
            .WithMessage(CancelCustomerInvoiceRequestValidationErrors.CustomerInvoiceCannotBeDeleted);
}