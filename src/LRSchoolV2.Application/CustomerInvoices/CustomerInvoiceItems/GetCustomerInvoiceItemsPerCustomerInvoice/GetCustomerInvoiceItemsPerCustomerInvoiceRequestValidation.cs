using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CustomerInvoices.CustomerInvoiceItems.GetCustomerInvoiceItemsPerCustomerInvoice;

public class GetCustomerInvoiceItemsPerCustomerInvoiceRequestValidation(ICustomerInvoicesRepository inCustomerInvoicesRepository) : AbstractValidator<GetCustomerInvoiceItemsPerCustomerInvoiceRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<GetCustomerInvoiceItemsPerCustomerInvoiceRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateServiceId();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateServiceId() =>
        RuleFor(inRequest => inRequest.CustomerInvoiceId)
            .MustAsync((inCustomerInvoiceId, _) => inCustomerInvoicesRepository.AnyCustomerInvoiceAsync(inCustomerInvoiceId))
            .WithMessage(GetCustomerInvoiceItemsPerCustomerInvoiceRequestValidationErrors.CustomerInvoiceNotFound);
}