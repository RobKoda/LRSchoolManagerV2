using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.Persistence;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.CustomerQuotes.CustomerQuoteItems.SaveCustomerQuoteItem;

public class SaveCustomerQuoteItemRequestValidation(
    ICustomerQuotesRepository inCustomerQuotesRepository
) : AbstractValidator<SaveCustomerQuoteItemRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<SaveCustomerQuoteItemRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateCustomerQuote();
        ValidateQuantity();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateCustomerQuote() =>
        RuleFor(inRequest => inRequest.CustomerQuoteItem.CustomerQuote.Id)
            .MustAsync((inCustomerQuoteId, _) => inCustomerQuotesRepository.AnyCustomerQuoteAsync(inCustomerQuoteId))
            .WithMessage(SaveCustomerQuoteItemRequestValidationErrors.CustomerQuoteNotFound);

    private void ValidateQuantity() =>
        RuleFor(inRequest => inRequest.CustomerQuoteItem.Quantity)
            .Must(inQuantity => inQuantity > 0)
            .WithMessage(SaveCustomerQuoteItemRequestValidationErrors.InvalidQuantity);
}