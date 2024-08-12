using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.Persistence;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoiceItems.SaveConsultantInvoiceItem;

public class SaveConsultantInvoiceItemRequestValidation(
    IConsultantInvoicesRepository inConsultantInvoicesRepository
) : AbstractValidator<SaveConsultantInvoiceItemRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<SaveConsultantInvoiceItemRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateConsultantInvoice();
        ValidateQuantity();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateConsultantInvoice() =>
        RuleFor(inRequest => inRequest.ConsultantInvoiceItem.ConsultantInvoice.Id)
            .MustAsync((inConsultantInvoiceId, _) => inConsultantInvoicesRepository.AnyConsultantInvoiceAsync(inConsultantInvoiceId))
            .WithMessage(SaveConsultantInvoiceItemRequestValidationErrors.ConsultantInvoiceNotFound);

    private void ValidateQuantity() =>
        RuleFor(inRequest => inRequest.ConsultantInvoiceItem.Quantity)
            .Must(inQuantity => inQuantity > 0)
            .WithMessage(SaveConsultantInvoiceItemRequestValidationErrors.InvalidQuantity);
}