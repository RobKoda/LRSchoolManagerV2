using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.ConsultantQuotes.ConsultantQuotes.Persistence;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantQuotes.ConsultantQuoteItems.SaveConsultantQuoteItem;

public class SaveConsultantQuoteItemRequestValidation(
    IConsultantQuotesRepository inConsultantQuotesRepository
) : AbstractValidator<SaveConsultantQuoteItemRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<SaveConsultantQuoteItemRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateConsultantQuote();
        ValidateQuantity();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateConsultantQuote() =>
        RuleFor(inRequest => inRequest.ConsultantQuoteItem.ConsultantQuote.Id)
            .MustAsync((inConsultantQuoteId, _) => inConsultantQuotesRepository.AnyConsultantQuoteAsync(inConsultantQuoteId))
            .WithMessage(SaveConsultantQuoteItemRequestValidationErrors.ConsultantQuoteNotFound);

    private void ValidateQuantity() =>
        RuleFor(inRequest => inRequest.ConsultantQuoteItem.Quantity)
            .Must(inQuantity => inQuantity > 0)
            .WithMessage(SaveConsultantQuoteItemRequestValidationErrors.InvalidQuantity);
}