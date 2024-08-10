using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.Common.SchoolYears.Persistence;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.Common.SchoolYears.DeleteSchoolYear;

public class DeleteSchoolYearRequestValidation(
    ISchoolYearsRepository inRepository
    ) : AbstractValidator<DeleteSchoolYearRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<DeleteSchoolYearRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateId();
        ValidateNotBetweenSchoolYears();
        ValidateCanBeDeleted();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateId() =>
        RuleFor(inRequest => inRequest.Id)
            .MustAsync((inSchoolYearId, _) => inRepository.AnySchoolYearAsync(inSchoolYearId))
            .WithMessage(DeleteSchoolYearRequestValidationErrors.SchoolYearNotFound);
    
    private void ValidateNotBetweenSchoolYears() =>
        RuleFor(inRequest => inRequest.Id)
            .MustAsync(async (inId, _) =>
            {
                var previousSchoolYear = await inRepository.GetPreviousSchoolYearAsync(inId);
                var nextSchoolYear = await inRepository.GetNextSchoolYearAsync(inId);
                return previousSchoolYear.IsNone || nextSchoolYear.IsNone;
            })
            .WithMessage(DeleteSchoolYearRequestValidationErrors.SchoolYearBetweenTwoSchoolYears);
    
    private void ValidateCanBeDeleted() =>
        RuleFor(inRequest => inRequest.Id)
            .MustAsync((inSchoolYearId, _) => inRepository.CanSchoolYearBeDeletedAsync(inSchoolYearId))
            .WithMessage(DeleteSchoolYearRequestValidationErrors.SchoolYearCannotBeDeleted);
}