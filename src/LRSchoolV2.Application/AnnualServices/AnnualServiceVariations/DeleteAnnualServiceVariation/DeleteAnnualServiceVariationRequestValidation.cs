using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.AnnualServices.AnnualServiceVariations.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariations.DeleteAnnualServiceVariation;

public class DeleteAnnualServiceVariationRequestValidation(IAnnualServiceVariationsRepository inAnnualServiceVariationsRepository) : AbstractValidator<DeleteAnnualServiceVariationRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<DeleteAnnualServiceVariationRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateId();
        ValidateCanBeDeleted();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateId() =>
        RuleFor(inRequest => inRequest.AnnualServiceVariation.Id)
            .MustAsync((inServiceVariationId, _) => inAnnualServiceVariationsRepository.AnyAnnualServiceVariationAsync(inServiceVariationId))
            .WithMessage(DeleteAnnualServiceVariationRequestValidationErrors.AnnualServiceVariationNotFound);
    
    private void ValidateCanBeDeleted() =>
        RuleFor(inRequest => inRequest.AnnualServiceVariation.Id)
            .MustAsync((inServiceVariationId, _) => inAnnualServiceVariationsRepository.CanAnnualServiceVariationBeDeletedAsync(inServiceVariationId))
            .WithMessage(DeleteAnnualServiceVariationRequestValidationErrors.AnnualServiceVariationCannotBeDeleted);
}