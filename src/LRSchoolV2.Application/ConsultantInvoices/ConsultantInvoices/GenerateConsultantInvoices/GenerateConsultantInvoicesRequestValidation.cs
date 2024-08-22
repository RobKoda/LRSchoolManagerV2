using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.Consultants.Persistence;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.GenerateConsultantInvoices;

public class GenerateConsultantInvoicesRequestValidation(
    IConsultantsRepository inConsultantsRepository
) : AbstractValidator<GenerateConsultantInvoicesRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<GenerateConsultantInvoicesRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateConsultantsInvoiceDocumentExists();
        
        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateConsultantsInvoiceDocumentExists() =>
        RuleFor(inRequest => inRequest.ConsultantInvoiceables)
            .MustAsync(async (inInvoiceables, _) =>
            {
                foreach (var consultant in inInvoiceables.Select(inInvoiceable => inInvoiceable.Consultant).Distinct())
                {
                    var document = await inConsultantsRepository.GetConsultantInvoiceDocument(consultant.Id);
                    var result = document.Match(inSome => inSome.Length > 0,
                    () => false);
                    
                    if (!result) return false;
                }
                
                return true;
            })
            .WithMessage(GenerateConsultantInvoicesRequestValidationErrors.ConsultantNoInvoiceDocument);
}