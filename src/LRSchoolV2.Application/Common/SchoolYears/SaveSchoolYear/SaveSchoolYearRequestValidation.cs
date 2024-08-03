using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.Common.SchoolYears.Persistence;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.Common.SchoolYears.SaveSchoolYear;

public class SaveSchoolYearRequestValidation(ISchoolYearsRepository inRepository) : AbstractValidator<SaveSchoolYearRequest>
{
    public override async Task<ValidationResult> ValidateAsync(ValidationContext<SaveSchoolYearRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateSchoolYear();
        ValidateMembershipFee();
        
        return await base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateSchoolYear()
    {
        ValidateStartDateRightAfterPreviousEndDate();
        ValidateEndDateRightAfterNextStartDate();
    }
    
    private void ValidateStartDateRightAfterPreviousEndDate() =>
        RuleFor(inRequest => inRequest.SchoolYear)
            .MustAsync(async (inSchoolYear, _) => (await inRepository.GetPreviousSchoolYearAsync(inSchoolYear.Id))
                .Match(inSome => inSome.EndDate.AddDays(1) == inSchoolYear.StartDate,
                    () => true))
            .WithMessage(SaveSchoolYearRequestValidationErrors.SchoolYearStartDateNotRightAfterPreviousEndDate);
    
    private void ValidateEndDateRightAfterNextStartDate() =>
        RuleFor(inRequest => inRequest.SchoolYear)
            .MustAsync(async (inSchoolYear, _) => (await inRepository.GetNextSchoolYearAsync(inSchoolYear.Id))
                .Match(inSome => inSome.StartDate.AddDays(-1) == inSchoolYear.EndDate,
                () => true))
            .WithMessage(SaveSchoolYearRequestValidationErrors.SchoolYearEndDateNotRightBeforeNextStartDate);
    
    private void ValidateMembershipFee() =>
        ValidateMembershipFeeIsPositive();
    
    private void ValidateMembershipFeeIsPositive() =>
        RuleFor(inRequest => inRequest.SchoolYear.MembershipFee)
            .Must(inMembershipFee => inMembershipFee > 0)
            .WithMessage(SaveSchoolYearRequestValidationErrors.SchoolYearMembershipFeeNotPositive);
}