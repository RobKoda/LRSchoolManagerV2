using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.AnnualServices.AnnualServiceVariations.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariationYearlyPrices.GetAnnualServiceVariationYearlyPricesPerAnnualServiceVariation;

public class GetAnnualServiceVariationYearlyPricesPerAnnualServiceVariationRequestValidation(IAnnualServiceVariationsRepository inAnnualServiceVariationsRepository)
    : AbstractValidator<GetAnnualServiceVariationYearlyPricesPerAnnualServiceVariationRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<GetAnnualServiceVariationYearlyPricesPerAnnualServiceVariationRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateId();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateId() =>
        RuleFor(inRequest => inRequest.AnnualServiceVariationId)
            .MustAsync((inServiceVariationId, _) => inAnnualServiceVariationsRepository.AnyAnnualServiceVariationAsync(inServiceVariationId))
            .WithMessage(GetAnnualServiceVariationYearlyPricesPerAnnualServiceVariationRequestValidationErrors.AnnualServiceVariationNotFound);
}