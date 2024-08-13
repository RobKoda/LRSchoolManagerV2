using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.ConsultantQuotes.ConsultantQuotes.Persistence;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantQuotes.ConsultantQuotes.CancelConsultantQuote;

public class CancelConsultantQuoteRequestValidation(
    IConsultantQuotesRepository inConsultantQuotesRepository
) : AbstractValidator<CancelConsultantQuoteRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<CancelConsultantQuoteRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateIsLastConsultantQuote();
        ValidateCanBeDeleted();
        
        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateIsLastConsultantQuote() =>
        RuleFor(inRequest => inRequest.ConsultantQuote)
            .MustAsync(async (inQuote, _) =>
                (await inConsultantQuotesRepository.GetLastConsultantQuoteAsync(inQuote.Consultant.Id))
                .Match(inSome => inQuote.Id == inSome.Id, () => false)
            )
            .WithMessage(CancelConsultantQuoteRequestValidationErrors.NotTheLastConsultantQuote);
    
    private void ValidateCanBeDeleted() =>
        RuleFor(inRequest => inRequest.ConsultantQuote.Id)
            .MustAsync((inQuoteId, _) => inConsultantQuotesRepository.CanConsultantQuoteBeDeletedAsync(inQuoteId))
            .WithMessage(CancelConsultantQuoteRequestValidationErrors.ConsultantQuoteCannotBeDeleted);
}