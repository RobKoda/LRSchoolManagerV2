using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.AnnualServices.AnnualServiceVariationConsultantWorks.Persistence;
using LRSchoolV2.Application.Common.SchoolYears.Persistence;
using LRSchoolV2.Application.Consultants.Consultants.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariationConsultantWorks.SaveAnnualServiceVariationConsultantWork;

public class SaveAnnualServiceVariationConsultantWorkRequestValidation(
    IAnnualServiceVariationConsultantWorksRepository inAnnualServiceVariationConsultantWorksRepository,
    ISchoolYearsRepository inSchoolYearsRepository,
    IConsultantsRepository inConsultantsRepository)
    : AbstractValidator<SaveAnnualServiceVariationConsultantWorkRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<SaveAnnualServiceVariationConsultantWorkRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateSchoolYear();
        ValidateConsultant();
        ValidateUniqueness();
        
        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateSchoolYear() =>
        RuleFor(inRequest => inRequest.AnnualServiceVariationConsultantWork.SchoolYear.Id)
            .MustAsync((inSchoolYearId, _) => inSchoolYearsRepository.AnySchoolYearAsync(inSchoolYearId))
            .WithMessage(SaveAnnualServiceVariationConsultantWorkRequestValidationErrors.SchoolYearNotFound);
    
    private void ValidateConsultant() =>
        RuleFor(inRequest => inRequest.AnnualServiceVariationConsultantWork.Consultant.Id)
            .MustAsync((inConsultantId, _) => inConsultantsRepository.AnyConsultantAsync(inConsultantId))
            .WithMessage(SaveAnnualServiceVariationConsultantWorkRequestValidationErrors.ConsultantNotFound);
    
    private void ValidateUniqueness() =>
        RuleFor(inRequest => inRequest.AnnualServiceVariationConsultantWork)
            .MustAsync((inServiceVariationConsultantWork, _) => inAnnualServiceVariationConsultantWorksRepository.IsAnnualServiceVariationConsultantWorkUniqueAsync(inServiceVariationConsultantWork))
            .WithMessage(SaveAnnualServiceVariationConsultantWorkRequestValidationErrors.AnnualServiceVariationConsultantWorkNotUnique);
}