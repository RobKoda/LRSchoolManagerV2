using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.ConsultantQuotes.ConsultantQuotes.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.ConsultantQuotes.ConsultantQuoteItems.GetConsultantQuoteItemsPerConsultantQuote;

public class GetConsultantQuoteItemsPerConsultantQuoteRequestValidation(IConsultantQuotesRepository inConsultantQuotesRepository) : AbstractValidator<GetConsultantQuoteItemsPerConsultantQuoteRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<GetConsultantQuoteItemsPerConsultantQuoteRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateServiceId();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateServiceId() =>
        RuleFor(inRequest => inRequest.ConsultantQuoteId)
            .MustAsync((inConsultantQuoteId, _) => inConsultantQuotesRepository.AnyConsultantQuoteAsync(inConsultantQuoteId))
            .WithMessage(GetConsultantQuoteItemsPerConsultantQuoteRequestValidationErrors.ConsultantQuoteNotFound);
}