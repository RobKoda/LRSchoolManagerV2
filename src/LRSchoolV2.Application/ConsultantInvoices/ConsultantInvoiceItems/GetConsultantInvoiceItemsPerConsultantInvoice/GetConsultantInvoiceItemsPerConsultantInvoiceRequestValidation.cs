using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoiceItems.GetConsultantInvoiceItemsPerConsultantInvoice;

public class GetConsultantInvoiceItemsPerConsultantInvoiceRequestValidation(IConsultantInvoicesRepository inConsultantInvoicesRepository) : AbstractValidator<GetConsultantInvoiceItemsPerConsultantInvoiceRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<GetConsultantInvoiceItemsPerConsultantInvoiceRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateServiceId();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateServiceId() =>
        RuleFor(inRequest => inRequest.ConsultantInvoiceId)
            .MustAsync((inConsultantInvoiceId, _) => inConsultantInvoicesRepository.AnyConsultantInvoiceAsync(inConsultantInvoiceId))
            .WithMessage(GetConsultantInvoiceItemsPerConsultantInvoiceRequestValidationErrors.ConsultantInvoiceNotFound);
}