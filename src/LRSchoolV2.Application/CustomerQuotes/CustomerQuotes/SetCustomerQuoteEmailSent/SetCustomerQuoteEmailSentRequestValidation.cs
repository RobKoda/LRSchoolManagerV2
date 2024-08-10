using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.Persistence;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.SetCustomerQuoteEmailSent;

public class SetCustomerQuoteEmailSentRequestValidation(
    ICustomerQuotesRepository inCustomerQuotesRepository
) : AbstractValidator<SetCustomerQuoteEmailSentRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<SetCustomerQuoteEmailSentRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateCustomerQuoteExistence();
        
        return base.ValidateAsync(inContext, inCancellation);
    }

    private void ValidateCustomerQuoteExistence() =>
        RuleFor(inRequest => inRequest.CustomerQuoteId)
            .MustAsync((inId, _) => inCustomerQuotesRepository.AnyCustomerQuoteAsync(inId))
            .WithMessage(SetCustomerQuoteEmailSentRequestValidationErrors.CustomerQuoteNotFound);
}