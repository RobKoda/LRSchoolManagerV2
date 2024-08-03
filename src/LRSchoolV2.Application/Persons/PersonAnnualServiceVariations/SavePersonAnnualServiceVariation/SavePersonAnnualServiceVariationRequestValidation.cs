using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.SavePersonAnnualServiceVariation;

public class SavePersonAnnualServiceVariationRequestValidation(IPersonAnnualServiceVariationsRepository inPersonAnnualServiceVariationsRepository) : AbstractValidator<SavePersonAnnualServiceVariationRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<SavePersonAnnualServiceVariationRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateUniqueness();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateUniqueness() =>
        RuleFor(inRequest => inRequest.PersonAnnualServiceVariation)
            .MustAsync((inPersonServiceVariation, _) => inPersonAnnualServiceVariationsRepository.IsPersonAnnualServiceVariationUniqueAsync(inPersonServiceVariation))
            .WithMessage(SavePersonAnnualServiceVariationRequestValidationErrors.PersonAnnualServiceVariationNotUnique);
}