using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.Persistence;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.CancelCustomerQuote;

public class CancelCustomerQuoteRequestValidation(
    ICustomerQuotesRepository inCustomerQuotesRepository
) : AbstractValidator<CancelCustomerQuoteRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<CancelCustomerQuoteRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateIsLastCustomerQuote();
        ValidateCanBeDeleted();
        
        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateIsLastCustomerQuote() =>
        RuleFor(inRequest => inRequest.CustomerQuote)
            .MustAsync(async (inQuote, _) =>
                (await inCustomerQuotesRepository.GetLastCustomerQuoteAsync())
                .Match(inSome => inQuote.Id == inSome.Id, () => false)
            )
            .WithMessage(CancelCustomerQuoteRequestValidationErrors.NotTheLastCustomerQuote);
    
    private void ValidateCanBeDeleted() =>
        RuleFor(inRequest => inRequest.CustomerQuote.Id)
            .MustAsync((inQuoteId, _) => inCustomerQuotesRepository.CanCustomerQuoteBeDeletedAsync(inQuoteId))
            .WithMessage(CancelCustomerQuoteRequestValidationErrors.CustomerQuoteCannotBeDeleted);
}