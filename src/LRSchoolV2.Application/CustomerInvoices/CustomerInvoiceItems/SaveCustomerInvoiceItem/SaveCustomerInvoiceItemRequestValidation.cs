using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CustomerInvoices.CustomerInvoiceItems.SaveCustomerInvoiceItem;

public class SaveCustomerInvoiceItemRequestValidation : AbstractValidator<SaveCustomerInvoiceItemRequest>
{
    private readonly ICustomerInvoicesRepository _customerInvoicesRepository;

    public SaveCustomerInvoiceItemRequestValidation(ICustomerInvoicesRepository inCustomerInvoicesRepository)
    {
        _customerInvoicesRepository = inCustomerInvoicesRepository;
    }
    
    public override Task<ValidationResult> ValidateAsync(ValidationContext<SaveCustomerInvoiceItemRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateCustomerInvoice();
        ValidateQuantity();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateCustomerInvoice() =>
        RuleFor(inRequest => inRequest.CustomerInvoiceItem.CustomerInvoice.Id)
            .MustAsync((inCustomerInvoiceId, _) => _customerInvoicesRepository.AnyCustomerInvoiceAsync(inCustomerInvoiceId))
            .WithMessage(SaveCustomerInvoiceItemRequestValidationErrors.CustomerInvoiceNotFound);

    private void ValidateQuantity() =>
        RuleFor(inRequest => inRequest.CustomerInvoiceItem.Quantity)
            .Must(inQuantity => inQuantity > 0)
            .WithMessage(SaveCustomerInvoiceItemRequestValidationErrors.InvalidQuantity);
}