using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.AnnualServices.AnnualServices.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServices.DeleteAnnualService;

public class DeleteAnnualServiceRequestValidation(IAnnualServicesRepository inRepository) : AbstractValidator<DeleteAnnualServiceRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<DeleteAnnualServiceRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateId();
        ValidateCanBeDeleted();
        
        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateId() =>
        RuleFor(inRequest => inRequest.AnnualService.Id)
            .MustAsync((inServiceId, _) => inRepository.AnyAnnualServiceAsync(inServiceId))
            .WithMessage(DeleteAnnualServiceRequestValidationErrors.AnnualServiceNotFound);
    
    private void ValidateCanBeDeleted() =>
        RuleFor(inRequest => inRequest.AnnualService)
            .MustAsync((inService, _) => inRepository.CanAnnualServiceBeDeletedAsync(inService.Id))
            .WithMessage(DeleteAnnualServiceRequestValidationErrors.AnnualServiceCannotBeDeleted);
}