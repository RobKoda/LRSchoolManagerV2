using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.AnnualServices.AnnualServiceVariationYearlyPrices.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariationYearlyPrices.DeleteAnnualServiceVariationYearlyPrice;

public class DeleteAnnualServiceVariationYearlyPriceRequestValidation(IAnnualServiceVariationYearlyPricesRepository inAnnualServiceVariationYearlyPricesRepository) : AbstractValidator<DeleteAnnualServiceVariationYearlyPriceRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<DeleteAnnualServiceVariationYearlyPriceRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateId();
        ValidateCanBeDeleted();
        
        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateId() =>
        RuleFor(inRequest => inRequest.AnnualServiceVariationYearlyPrice.Id)
            .MustAsync((inServiceVariationYearlyPriceId, _) => inAnnualServiceVariationYearlyPricesRepository.AnyAnnualServiceVariationYearlyPriceAsync(inServiceVariationYearlyPriceId))
            .WithMessage(DeleteAnnualServiceVariationYearlyPriceRequestValidationErrors.AnnualServiceVariationYearlyPriceNotFound);
    
    private void ValidateCanBeDeleted() =>
        RuleFor(inRequest => inRequest.AnnualServiceVariationYearlyPrice.Id)
            .MustAsync((inServiceVariationYearlyPriceId, _) => inAnnualServiceVariationYearlyPricesRepository.CanAnnualServiceVariationYearlyPriceBeDeletedAsync(inServiceVariationYearlyPriceId))
            .WithMessage(DeleteAnnualServiceVariationYearlyPriceRequestValidationErrors.AnnualServiceVariationYearlyPriceCannotBeDeleted);
}