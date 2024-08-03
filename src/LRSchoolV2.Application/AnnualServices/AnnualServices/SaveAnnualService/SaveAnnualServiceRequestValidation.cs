using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.AnnualServices.AnnualServices.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServices.SaveAnnualService;

public class SaveAnnualServiceRequestValidation(IAnnualServicesRepository inRepository) : AbstractValidator<SaveAnnualServiceRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<SaveAnnualServiceRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateUniqueness();
        
        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateUniqueness() =>
        RuleFor(inRequest => inRequest.AnnualService)
            .MustAsync((inService, _) => inRepository.IsAnnualServiceUniqueAsync(inService))
            .WithMessage(SaveAnnualServiceRequestValidationErrors.AnnualServiceNotUnique);
}