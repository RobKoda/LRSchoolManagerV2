using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.AnnualServices.AnnualServiceVariationConsultantWorks.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariationConsultantWorks.DeleteAnnualServiceVariationConsultantWork;

public class DeleteAnnualServiceVariationConsultantWorkRequestValidation(IAnnualServiceVariationConsultantWorksRepository inAnnualServiceVariationConsultantWorksRepository)
    : AbstractValidator<DeleteAnnualServiceVariationConsultantWorkRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<DeleteAnnualServiceVariationConsultantWorkRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateId();
        ValidateCanBeDeleted();
        
        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateId() =>
        RuleFor(inRequest => inRequest.AnnualServiceVariationConsultantWork.Id)
            .MustAsync((inServiceVariationConsultantWorkId, _) => inAnnualServiceVariationConsultantWorksRepository.AnyAnnualServiceVariationConsultantWorkAsync(inServiceVariationConsultantWorkId))
            .WithMessage(DeleteAnnualServiceVariationConsultantWorkRequestValidationErrors.AnnualServiceVariationConsultantWorkNotFound);
    
    private void ValidateCanBeDeleted() =>
        RuleFor(inRequest => inRequest.AnnualServiceVariationConsultantWork.Id)
            .MustAsync((inServiceVariationConsultantWorkId, _) => inAnnualServiceVariationConsultantWorksRepository.CanAnnualServiceVariationConsultantWorkBeDeletedAsync(inServiceVariationConsultantWorkId))
            .WithMessage(DeleteAnnualServiceVariationConsultantWorkRequestValidationErrors.AnnualServiceVariationConsultantWorkCannotBeDeleted);
}