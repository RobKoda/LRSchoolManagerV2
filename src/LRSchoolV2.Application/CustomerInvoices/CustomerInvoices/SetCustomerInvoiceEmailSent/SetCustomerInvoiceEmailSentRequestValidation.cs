using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.SetCustomerInvoiceEmailSent;

public class SetCustomerInvoiceEmailSentRequestValidation : AbstractValidator<SetCustomerInvoiceEmailSentRequest>
{
    private readonly ICustomerInvoicesRepository _customerInvoicesRepository;

    public SetCustomerInvoiceEmailSentRequestValidation(ICustomerInvoicesRepository inCustomerInvoicesRepository)
    {
        _customerInvoicesRepository = inCustomerInvoicesRepository;
    }

    public override Task<ValidationResult> ValidateAsync(ValidationContext<SetCustomerInvoiceEmailSentRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateCustomerInvoiceExistence();
        
        return base.ValidateAsync(inContext, inCancellation);
    }

    private void ValidateCustomerInvoiceExistence() =>
        RuleFor(inRequest => inRequest.CustomerInvoiceId)
            .MustAsync((inId, _) => _customerInvoicesRepository.AnyCustomerInvoiceAsync(inId))
            .WithMessage(SetCustomerInvoiceEmailSentRequestValidationErrors.CustomerInvoiceNotFound);
}