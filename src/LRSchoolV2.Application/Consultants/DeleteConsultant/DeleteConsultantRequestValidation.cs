using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.Consultants.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Consultants.DeleteConsultant;

public class DeleteConsultantRequestValidation(IConsultantsRepository inRepository) : AbstractValidator<DeleteConsultantRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<DeleteConsultantRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateId();
        ValidateCanBeDeleted();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateId() =>
        RuleFor(inRequest => inRequest.Consultant.Id)
            .MustAsync((inConsultantId, _) => inRepository.AnyConsultantAsync(inConsultantId))
            .WithMessage(DeleteConsultantRequestValidationErrors.ConsultantNotFound);
    
    private void ValidateCanBeDeleted() =>
        RuleFor(inRequest => inRequest.Consultant)
            .MustAsync((inConsultant, _) => inRepository.CanConsultantBeDeleted(inConsultant.Id))
            .WithMessage(DeleteConsultantRequestValidationErrors.ConsultantCannotBeDeleted);
}