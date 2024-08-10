using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CustomerQuotes.CustomerQuoteItems.GetCustomerQuoteItemsPerCustomerQuote;

public class GetCustomerQuoteItemsPerCustomerQuoteRequestValidation(ICustomerQuotesRepository inCustomerQuotesRepository) : AbstractValidator<GetCustomerQuoteItemsPerCustomerQuoteRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<GetCustomerQuoteItemsPerCustomerQuoteRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateServiceId();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateServiceId() =>
        RuleFor(inRequest => inRequest.CustomerQuoteId)
            .MustAsync((inCustomerQuoteId, _) => inCustomerQuotesRepository.AnyCustomerQuoteAsync(inCustomerQuoteId))
            .WithMessage(GetCustomerQuoteItemsPerCustomerQuoteRequestValidationErrors.CustomerQuoteNotFound);
}