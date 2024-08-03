using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.AnnualServices.AnnualServices.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariations.GetAnnualServiceVariationsPerService;

public class GetAnnualServiceVariationsPerAnnualServiceRequestValidation(IAnnualServicesRepository inAnnualServicesRepository) : AbstractValidator<GetAnnualServiceVariationsPerServiceRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<GetAnnualServiceVariationsPerServiceRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateServiceId();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateServiceId() =>
        RuleFor(inRequest => inRequest.AnnualServiceId)
            .MustAsync((inServiceId, _) => inAnnualServicesRepository.AnyAnnualServiceAsync(inServiceId))
            .WithMessage(GetAnnualServiceVariationsPerAnnualServiceRequestValidationErrors.AnnualServiceNotFound);
}