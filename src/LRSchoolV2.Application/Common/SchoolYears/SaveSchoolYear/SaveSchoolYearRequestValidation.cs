using FluentValidation;
using FluentValidation.Results;
using LanguageExt;
using LRSchoolV2.Application.Common.SchoolYears.Persistence;
using LRSchoolV2.Domain.Common;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Common.SchoolYears.SaveSchoolYear;

public class SaveSchoolYearRequestValidation(ISchoolYearsRepository inRepository) : AbstractValidator<SaveSchoolYearRequest>
{
    public override async Task<ValidationResult> ValidateAsync(ValidationContext<SaveSchoolYearRequest> inContext,
        CancellationToken inCancellation = new())
    {
        var previousSchoolYear = await inRepository.GetPreviousSchoolYearAsync(inContext.InstanceToValidate.SchoolYear.Id);
        var nextSchoolYear = await inRepository.GetNextSchoolYearAsync(inContext.InstanceToValidate.SchoolYear.Id);
        
        ValidateSchoolYear(previousSchoolYear, nextSchoolYear);
        ValidateMembershipFee();
        
        return await base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateSchoolYear(Option<SchoolYear> inPreviousSchoolYear, Option<SchoolYear> inNextSchoolYear)
    {
        ValidateStartDateRightAfterPreviousEndDate(inPreviousSchoolYear);
        ValidateEndDateRightAfterNextStartDate(inNextSchoolYear);
    }

    private void ValidateStartDateRightAfterPreviousEndDate(Option<SchoolYear> inPreviousSchoolYear) =>
        RuleFor(inRequest => inRequest.SchoolYear.StartDate)
            .Must(inStartDate => inPreviousSchoolYear.Match(
                inSome => inSome.EndDate.AddDays(1) == inStartDate,
                () => true))
            .WithMessage(SaveSchoolYearRequestValidationErrors.SchoolYearStartDateNotRightAfterPreviousEndDate);

    private void ValidateEndDateRightAfterNextStartDate(Option<SchoolYear> inNextSchoolYear) =>
        RuleFor(inRequest => inRequest.SchoolYear.EndDate)
            .Must(inEndDate => inNextSchoolYear.Match(
                inSome => inSome.StartDate.AddDays(-1) == inEndDate,
                () => true))
            .WithMessage(SaveSchoolYearRequestValidationErrors.SchoolYearEndDateNotRightBeforeNextStartDate);
    
    private void ValidateMembershipFee() =>
        ValidateMembershipFeeIsPositive();
    
    private void ValidateMembershipFeeIsPositive() =>
        RuleFor(inRequest => inRequest.SchoolYear.MembershipFee)
            .Must(inMembershipFee => inMembershipFee > 0)
            .WithMessage(SaveSchoolYearRequestValidationErrors.SchoolYearMembershipFeeNotPositive);
}