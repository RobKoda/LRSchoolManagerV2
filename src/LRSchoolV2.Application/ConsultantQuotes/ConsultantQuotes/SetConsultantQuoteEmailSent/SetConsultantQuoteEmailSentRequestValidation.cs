using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.ConsultantQuotes.ConsultantQuotes.Persistence;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantQuotes.ConsultantQuotes.SetConsultantQuoteEmailSent;

public class SetConsultantQuoteEmailSentRequestValidation(
    IConsultantQuotesRepository inConsultantQuotesRepository
) : AbstractValidator<SetConsultantQuoteEmailSentRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<SetConsultantQuoteEmailSentRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateConsultantQuoteExistence();
        
        return base.ValidateAsync(inContext, inCancellation);
    }

    private void ValidateConsultantQuoteExistence() =>
        RuleFor(inRequest => inRequest.ConsultantQuoteId)
            .MustAsync((inId, _) => inConsultantQuotesRepository.AnyConsultantQuoteAsync(inId))
            .WithMessage(SetConsultantQuoteEmailSentRequestValidationErrors.ConsultantQuoteNotFound);
}