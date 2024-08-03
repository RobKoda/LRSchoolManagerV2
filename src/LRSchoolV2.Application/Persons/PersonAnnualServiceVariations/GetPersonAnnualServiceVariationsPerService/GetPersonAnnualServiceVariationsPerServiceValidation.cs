using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.AnnualServices.AnnualServices.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.GetPersonAnnualServiceVariationsPerService;

public class GetPersonAnnualServiceVariationsPerServiceValidation(IAnnualServicesRepository inAnnualServicesRepository) : AbstractValidator<GetPersonAnnualServiceVariationsPerServiceRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<GetPersonAnnualServiceVariationsPerServiceRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidatePersonId();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidatePersonId() =>
        RuleFor(inRequest => inRequest.AnnualServiceId)
            .MustAsync((inServiceId, _) => inAnnualServicesRepository.AnyAnnualServiceAsync(inServiceId))
            .WithMessage(GetPersonAnnualServiceVariationsPerServiceValidationErrors.AnnualServiceNotFound);
}