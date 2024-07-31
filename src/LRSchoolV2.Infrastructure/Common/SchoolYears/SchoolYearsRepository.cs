using LanguageExt;
using LRSchoolV2.Application.Common.SchoolYears.Persistence;
using LRSchoolV2.Domain.Common;
using Microsoft.EntityFrameworkCore;
// ReSharper disable UnusedType.Global - Implicit use through assembly scan

namespace LRSchoolV2.Infrastructure.Common.SchoolYears;

public class SchoolYearsRepository(
    IDbContextFactory<ApplicationContext> inContext
    ) : ISchoolYearsRepository
{
    public Task<IEnumerable<SchoolYear>> GetSchoolYearsAsync() =>
        inContext.GetAllAsync<SchoolYearDataModel, SchoolYear>();

    public async Task<Option<SchoolYear>> GetCurrentSchoolYearAsync()
    {
        var theCurrentSchoolYear = (await inContext.GetAllAsync<SchoolYearDataModel, SchoolYear>(inQueryable => inQueryable
                .Where(inSchoolYear => inSchoolYear.StartDate < DateTime.Today && inSchoolYear.EndDate > DateTime.Today)))
            .SingleOrDefault();
        return theCurrentSchoolYear == default
            ? Option<SchoolYear>.None
            : Option<SchoolYear>.Some(theCurrentSchoolYear);
    }

    public async Task<Option<SchoolYear>> GetPreviousSchoolYearAsync(SchoolYear inReferenceSchoolYear)
    {
        var thePreviousSchoolYear = (await inContext.GetAllAsync<SchoolYearDataModel, SchoolYear>(inQueryable => inQueryable
                .OrderByDescending(inSchoolYear => inSchoolYear.StartDate)
                .Where(inSchoolYear => inSchoolYear.StartDate < inReferenceSchoolYear.StartDate)
            ))
            .FirstOrDefault();
        return thePreviousSchoolYear == default
            ? Option<SchoolYear>.None
            : Option<SchoolYear>.Some(thePreviousSchoolYear);
    }

    public async Task<Option<SchoolYear>> GetNextSchoolYearAsync(SchoolYear inReferenceSchoolYear)
    {
        var thePreviousSchoolYear = (await inContext.GetAllAsync<SchoolYearDataModel, SchoolYear>(inQueryable => inQueryable
                .OrderBy(inSchoolYear => inSchoolYear.StartDate)
                .Where(inSchoolYear => inSchoolYear.StartDate > inReferenceSchoolYear.StartDate)))
            .FirstOrDefault();
        return thePreviousSchoolYear == default
            ? Option<SchoolYear>.None
            : Option<SchoolYear>.Some(thePreviousSchoolYear);
    }

    public Task<bool> CanSchoolYearBeDeleted(Guid inSchoolYearId) => 
        inContext.CanBeDeleted<SchoolYearDataModel>(inSchoolYearId);

    public Task SaveSchoolYearAsync(SchoolYear inSchoolYear) =>
        inContext.SaveAsync<SchoolYearDataModel, SchoolYear>(inSchoolYear);

    public Task DeleteSchoolYearAsync(Guid inSchoolYearId) =>
        inContext.DeleteAsync<SchoolYearDataModel>(inSchoolYearId);

    public Task<bool> AnySchoolYearAsync(Guid inSchoolYearId) =>
        inContext.AnyAsync<SchoolYearDataModel>(inSchoolYearId);
}