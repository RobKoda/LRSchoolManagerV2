using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.AnnualServices.AnnualServices.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceConsultantWorks.GetAnnualServiceConsultantWorksPerAnnualService;

public class GetAnnualServiceConsultantWorksPerAnnualServiceRequestValidation(IAnnualServicesRepository inAnnualServicesRepository) : AbstractValidator<GetAnnualServiceConsultantWorksPerAnnualServiceRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<GetAnnualServiceConsultantWorksPerAnnualServiceRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateId();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateId() =>
        RuleFor(inRequest => inRequest.AnnualServiceId)
            .MustAsync((inServiceId, _) => inAnnualServicesRepository.AnyAnnualServiceAsync(inServiceId))
            .WithMessage(GetAnnualServiceConsultantWorksPerAnnualServiceRequestValidationErrors.AnnualServiceNotFound);
}