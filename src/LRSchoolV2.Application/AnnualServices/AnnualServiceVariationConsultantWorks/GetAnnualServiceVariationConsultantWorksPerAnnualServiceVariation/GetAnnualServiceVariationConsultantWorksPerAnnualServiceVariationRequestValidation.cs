using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.AnnualServices.AnnualServiceVariations.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariationConsultantWorks.GetAnnualServiceVariationConsultantWorksPerAnnualServiceVariation;

public class GetAnnualServiceVariationConsultantWorksPerAnnualServiceVariationRequestValidation(IAnnualServiceVariationsRepository inAnnualServiceVariationsRepository)
    : AbstractValidator<GetAnnualServiceVariationConsultantWorksPerAnnualServiceVariationRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<GetAnnualServiceVariationConsultantWorksPerAnnualServiceVariationRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateId();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateId() =>
        RuleFor(inRequest => inRequest.AnnualServiceVariationId)
            .MustAsync((inServiceVariationId, _) => inAnnualServiceVariationsRepository.AnyAnnualServiceVariationAsync(inServiceVariationId))
            .WithMessage(GetAnnualServiceVariationConsultantWorksPerAnnualServiceVariationRequestValidationErrors.AnnualServiceVariationNotFound);
}