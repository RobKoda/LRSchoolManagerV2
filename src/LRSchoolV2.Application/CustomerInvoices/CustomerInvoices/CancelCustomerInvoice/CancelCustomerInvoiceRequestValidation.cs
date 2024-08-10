using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.Persistence;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.CancelCustomerInvoice;

public class CancelCustomerInvoiceRequestValidation(
    ICustomerInvoicesRepository inCustomerInvoicesRepository
    ) : AbstractValidator<CancelCustomerInvoiceRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<CancelCustomerInvoiceRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateIsLastCustomerInvoice();
        ValidateCanBeDeleted();
        
        return base.ValidateAsync(inContext, inCancellation);
    }

    private void ValidateIsLastCustomerInvoice() =>
        RuleFor(inRequest => inRequest.CustomerInvoice)
            .MustAsync(async (inInvoice, _) =>
                (await inCustomerInvoicesRepository.GetLastCustomerInvoiceAsync())
                    .Match(inSome => inInvoice.Id == inSome.Id, () => false)
            )
            .WithMessage(CancelCustomerInvoiceRequestValidationErrors.NotTheLastCustomerInvoice);
    
    private void ValidateCanBeDeleted() =>
        RuleFor(inRequest => inRequest.CustomerInvoice.Id)
            .MustAsync((inInvoiceId, _) => inCustomerInvoicesRepository.CanCustomerInvoiceBeDeletedAsync(inInvoiceId))
            .WithMessage(CancelCustomerInvoiceRequestValidationErrors.CustomerInvoiceCannotBeDeleted);
}