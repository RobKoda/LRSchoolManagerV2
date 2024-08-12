using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.Persistence;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.SetConsultantInvoiceEmailSent;

public class SetConsultantInvoiceEmailSentRequestValidation(
    IConsultantInvoicesRepository inConsultantInvoicesRepository
) : AbstractValidator<SetConsultantInvoiceEmailSentRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<SetConsultantInvoiceEmailSentRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateConsultantInvoiceExistence();
        
        return base.ValidateAsync(inContext, inCancellation);
    }

    private void ValidateConsultantInvoiceExistence() =>
        RuleFor(inRequest => inRequest.ConsultantInvoiceId)
            .MustAsync((inId, _) => inConsultantInvoicesRepository.AnyConsultantInvoiceAsync(inId))
            .WithMessage(SetConsultantInvoiceEmailSentRequestValidationErrors.ConsultantInvoiceNotFound);
}