using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.AnnualServices.AnnualServiceConsultantWorks.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceConsultantWorks.DeleteAnnualServiceConsultantWork;

public class DeleteAnnualServiceConsultantWorkRequestValidation(IAnnualServiceConsultantWorksRepository inAnnualServiceConsultantWorksRepository) : AbstractValidator<DeleteAnnualServiceConsultantWorkRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<DeleteAnnualServiceConsultantWorkRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateId();
        ValidateCanBeDeleted();
        
        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateId() =>
        RuleFor(inRequest => inRequest.AnnualServiceConsultantWork.Id)
            .MustAsync((inServiceConsultantWorkId, _) => inAnnualServiceConsultantWorksRepository.AnyAnnualServiceConsultantWorkAsync(inServiceConsultantWorkId))
            .WithMessage(DeleteAnnualServiceConsultantWorkRequestValidationErrors.AnnualServiceConsultantWorkNotFound);
    
    private void ValidateCanBeDeleted() =>
        RuleFor(inRequest => inRequest.AnnualServiceConsultantWork.Id)
            .MustAsync((inServiceConsultantWorkId, _) => inAnnualServiceConsultantWorksRepository.CanAnnualServiceConsultantWorkBeDeletedAsync(inServiceConsultantWorkId))
            .WithMessage(DeleteAnnualServiceConsultantWorkRequestValidationErrors.AnnualServiceConsultantWorkCannotBeDeleted);
}