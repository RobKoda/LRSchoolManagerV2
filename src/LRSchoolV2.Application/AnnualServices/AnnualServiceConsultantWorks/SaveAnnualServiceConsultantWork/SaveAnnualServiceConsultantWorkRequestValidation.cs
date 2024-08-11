using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.AnnualServices.AnnualServiceConsultantWorks.Persistence;
using LRSchoolV2.Application.Common.SchoolYears.Persistence;
using LRSchoolV2.Application.Consultants.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceConsultantWorks.SaveAnnualServiceConsultantWork;

public class SaveAnnualServiceConsultantWorkRequestValidation(IAnnualServiceConsultantWorksRepository inAnnualServiceConsultantWorksRepository, ISchoolYearsRepository inSchoolYearsRepository, IConsultantsRepository inConsultantsRepository)
    : AbstractValidator<SaveAnnualServiceConsultantWorkRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<SaveAnnualServiceConsultantWorkRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateSchoolYear();
        ValidateConsultant();
        ValidateUniqueness();
        
        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateSchoolYear() =>
        RuleFor(inRequest => inRequest.AnnualServiceConsultantWork.SchoolYear.Id)
            .MustAsync((inSchoolYearId, _) => inSchoolYearsRepository.AnySchoolYearAsync(inSchoolYearId))
            .WithMessage(SaveAnnualServiceConsultantWorkRequestValidationErrors.SchoolYearNotFound);
    
    private void ValidateConsultant() =>
        RuleFor(inRequest => inRequest.AnnualServiceConsultantWork.Consultant.Id)
            .MustAsync((inConsultantId, _) => inConsultantsRepository.AnyConsultantAsync(inConsultantId))
            .WithMessage(SaveAnnualServiceConsultantWorkRequestValidationErrors.ConsultantNotFound);
    
    private void ValidateUniqueness() =>
        RuleFor(inRequest => inRequest.AnnualServiceConsultantWork)
            .MustAsync((inServiceConsultantWork, _) => inAnnualServiceConsultantWorksRepository.IsAnnualServiceConsultantWorkUniqueAsync(inServiceConsultantWork))
            .WithMessage(SaveAnnualServiceConsultantWorkRequestValidationErrors.AnnualServiceConsultantWorkNotUnique);
}