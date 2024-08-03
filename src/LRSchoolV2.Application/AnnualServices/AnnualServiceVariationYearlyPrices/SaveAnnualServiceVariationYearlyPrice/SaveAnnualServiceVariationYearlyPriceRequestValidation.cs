using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.AnnualServices.AnnualServiceVariationYearlyPrices.Persistence;
using LRSchoolV2.Application.Common.SchoolYears.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariationYearlyPrices.SaveAnnualServiceVariationYearlyPrice;

public class SaveAnnualServiceVariationYearlyPriceRequestValidation(IAnnualServiceVariationYearlyPricesRepository inAnnualServiceVariationYearlyPricesRepository, ISchoolYearsRepository inSchoolYearsRepository)
    : AbstractValidator<SaveAnnualServiceVariationYearlyPriceRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<SaveAnnualServiceVariationYearlyPriceRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateSchoolYear();
        ValidateUniqueness();
        
        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateSchoolYear() =>
        RuleFor(inRequest => inRequest.AnnualServiceVariationYearlyPrice.SchoolYear.Id)
            .MustAsync((inSchoolYearId, _) => inSchoolYearsRepository.AnySchoolYearAsync(inSchoolYearId))
            .WithMessage(SaveAnnualServiceVariationYearlyPriceRequestValidationErrors.SchoolYearNotFound);
    
    private void ValidateUniqueness() =>
        RuleFor(inRequest => inRequest.AnnualServiceVariationYearlyPrice)
            .MustAsync((inServiceVariationYearlyPrice, _) => inAnnualServiceVariationYearlyPricesRepository.IsAnnualServiceVariationYearlyPriceUniqueAsync(inServiceVariationYearlyPrice))
            .WithMessage(SaveAnnualServiceVariationYearlyPriceRequestValidationErrors.AnnualServiceVariationYearlyPriceNotUnique);
}