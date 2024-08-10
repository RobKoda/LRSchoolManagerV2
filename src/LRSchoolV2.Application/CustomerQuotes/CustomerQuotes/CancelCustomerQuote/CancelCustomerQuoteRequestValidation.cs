using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.Persistence;
using LRSchoolV2.Domain.CustomerQuotes;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.CancelCustomerQuote;

public class CancelCustomerQuoteRequestValidation(
    ICustomerQuotesRepository inCustomerQuotesRepository
) : AbstractValidator<CancelCustomerQuoteRequest>
{
    public override async Task<ValidationResult> ValidateAsync(ValidationContext<CancelCustomerQuoteRequest> inContext,
        CancellationToken inCancellation = new())
    {
        var customerQuotes = await inCustomerQuotesRepository.GetCustomerQuotesAsync();
        var lastCustomerQuote = customerQuotes.MaxBy(inQuote => inQuote.Date);
        ValidateIsLastCustomerQuote(lastCustomerQuote!);
        ValidateCanBeDeleted();
        
        return await base.ValidateAsync(inContext, inCancellation);
    }

    private void ValidateIsLastCustomerQuote(CustomerQuote inLastCustomerQuote) =>
        RuleFor(inRequest => inRequest.CustomerQuote)
            .Must(inQuote => inQuote.Id == inLastCustomerQuote.Id)
            .WithMessage(CancelCustomerQuoteRequestValidationErrors.NotTheLastCustomerQuote);
    
    private void ValidateCanBeDeleted() =>
        RuleFor(inRequest => inRequest.CustomerQuote.Id)
            .MustAsync((inQuoteId, _) => inCustomerQuotesRepository.CanCustomerQuoteBeDeletedAsync(inQuoteId))
            .WithMessage(CancelCustomerQuoteRequestValidationErrors.CustomerQuoteCannotBeDeleted);
}