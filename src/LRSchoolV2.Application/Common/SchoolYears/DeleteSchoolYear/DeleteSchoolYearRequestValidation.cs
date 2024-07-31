using FluentValidation;
using FluentValidation.Results;
using LanguageExt;
using LRSchoolV2.Application.Common.SchoolYears.Persistence;
using LRSchoolV2.Domain.Common;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Common.SchoolYears.DeleteSchoolYear;

public class DeleteSchoolYearRequestValidation(
    ISchoolYearsRepository inRepository
    ) : AbstractValidator<DeleteSchoolYearRequest>
{
    public override async Task<ValidationResult> ValidateAsync(ValidationContext<DeleteSchoolYearRequest> inContext,
        CancellationToken inCancellation = new())
    {
        var previousSchoolYear = await inRepository.GetPreviousSchoolYearAsync(inContext.InstanceToValidate.SchoolYear);
        var nextSchoolYear = await inRepository.GetNextSchoolYearAsync(inContext.InstanceToValidate.SchoolYear);
        
        ValidateId();
        ValidateNotBetweenSchoolYears(previousSchoolYear, nextSchoolYear);
        ValidateCanBeDeleted();

        return await base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateId() =>
        RuleFor(inRequest => inRequest.SchoolYear.Id)
            .MustAsync((inSchoolYearId, _) => inRepository.AnySchoolYearAsync(inSchoolYearId))
            .WithMessage(DeleteSchoolYearRequestValidationErrors.SchoolYearNotFound);
    
    private void ValidateNotBetweenSchoolYears(Option<SchoolYear> inPreviousSchoolYear, Option<SchoolYear> inNextSchoolYear) =>
        RuleFor(inRequest => inRequest.SchoolYear)
            .Must(_ => inPreviousSchoolYear.IsNone || inNextSchoolYear.IsNone)
            .WithMessage(DeleteSchoolYearRequestValidationErrors.SchoolYearBetweenTwoSchoolYears);
    
    private void ValidateCanBeDeleted() =>
        RuleFor(inRequest => inRequest.SchoolYear)
            .MustAsync((inSchoolYear, _) => inRepository.CanSchoolYearBeDeleted(inSchoolYear.Id))
            .WithMessage(DeleteSchoolYearRequestValidationErrors.SchoolYearCannotBeDeleted);
}