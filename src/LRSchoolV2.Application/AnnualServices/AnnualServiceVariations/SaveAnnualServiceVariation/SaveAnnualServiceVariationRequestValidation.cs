using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.AnnualServices.AnnualServices.Persistence;
using LRSchoolV2.Application.AnnualServices.AnnualServiceVariations.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariations.SaveAnnualServiceVariation;

public class SaveAnnualServiceVariationRequestValidation(IAnnualServiceVariationsRepository inAnnualServiceVariationsRepository, IAnnualServicesRepository inAnnualServicesRepository)
    : AbstractValidator<SaveAnnualServiceVariationRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<SaveAnnualServiceVariationRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateService();
        ValidateUniqueness();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateService() =>
        RuleFor(inRequest => inRequest.AnnualServiceVariation.AnnualService.Id)
            .MustAsync((inServiceId, _) => inAnnualServicesRepository.AnyAnnualServiceAsync(inServiceId))
            .WithMessage(SaveAnnualServiceVariationRequestValidationErrors.AnnualServiceNotFound);
    
    private void ValidateUniqueness() =>
        RuleFor(inRequest => inRequest.AnnualServiceVariation)
            .MustAsync((inServiceVariation, _) => inAnnualServiceVariationsRepository.IsAnnualServiceVariationUniqueAsync(inServiceVariation))
            .WithMessage(SaveAnnualServiceVariationRequestValidationErrors.AnnualServiceVariationNotUnique);
}