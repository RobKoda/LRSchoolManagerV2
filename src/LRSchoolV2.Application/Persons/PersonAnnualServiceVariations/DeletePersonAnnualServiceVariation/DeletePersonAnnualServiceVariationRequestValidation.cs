using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.DeletePersonAnnualServiceVariation;

public class DeletePersonAnnualServiceVariationRequestValidation(IPersonAnnualServiceVariationsRepository inPersonAnnualServiceVariationsRepository) : AbstractValidator<DeletePersonAnnualServiceVariationRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<DeletePersonAnnualServiceVariationRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateId();
        ValidateCanBeDeleted();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateId() =>
        RuleFor(inRequest => inRequest.PersonAnnualServiceVariation.Id)
            .MustAsync((inPersonServiceVariationId, _) => inPersonAnnualServiceVariationsRepository.AnyPersonAnnualServiceVariationAsync(inPersonServiceVariationId))
            .WithMessage(DeletePersonAnnualServiceVariationRequestValidationErrors.PersonAnnualServiceVariationNotFound);
    
    private void ValidateCanBeDeleted() =>
        RuleFor(inRequest => inRequest.PersonAnnualServiceVariation.Id)
            .MustAsync((inPersonServiceVariationId, _) => inPersonAnnualServiceVariationsRepository.CanPersonAnnualServiceVariationBeDeletedAsync(inPersonServiceVariationId))
            .WithMessage(DeletePersonAnnualServiceVariationRequestValidationErrors.PersonAnnualServiceVariationCannotBeDeleted);
}