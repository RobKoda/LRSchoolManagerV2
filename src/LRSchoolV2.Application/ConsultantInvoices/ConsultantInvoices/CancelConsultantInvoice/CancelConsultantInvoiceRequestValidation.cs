using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.Persistence;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.CancelConsultantInvoice;

public class CancelConsultantInvoiceRequestValidation(
    IConsultantInvoicesRepository inConsultantInvoicesRepository
) : AbstractValidator<CancelConsultantInvoiceRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<CancelConsultantInvoiceRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateIsLastConsultantInvoice();
        ValidateCanBeDeleted();
        
        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateIsLastConsultantInvoice() =>
        RuleFor(inRequest => inRequest.ConsultantInvoice)
            .MustAsync(async (inInvoice, _) =>
                (await inConsultantInvoicesRepository.GetLastConsultantInvoiceAsync(inInvoice.Consultant.Id))
                .Match(inSome => inInvoice.Id == inSome.Id, () => false)
            )
            .WithMessage(CancelConsultantInvoiceRequestValidationErrors.NotTheLastConsultantInvoice);
    
    private void ValidateCanBeDeleted() =>
        RuleFor(inRequest => inRequest.ConsultantInvoice.Id)
            .MustAsync((inInvoiceId, _) => inConsultantInvoicesRepository.CanConsultantInvoiceBeDeletedAsync(inInvoiceId))
            .WithMessage(CancelConsultantInvoiceRequestValidationErrors.ConsultantInvoiceCannotBeDeleted);
}