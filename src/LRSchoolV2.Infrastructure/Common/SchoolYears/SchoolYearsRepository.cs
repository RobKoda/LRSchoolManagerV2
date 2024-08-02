using LanguageExt;
using static LanguageExt.Prelude;
using LRSchoolV2.Application.Common.SchoolYears.Persistence;
using LRSchoolV2.Domain.Common;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global - Implicit use through assembly scan

namespace LRSchoolV2.Infrastructure.Common.SchoolYears;

public class SchoolYearsRepository(
    IDbContextFactory<ApplicationContext> inFactory
) : ISchoolYearsRepository
{
    public Task<IEnumerable<SchoolYear>> GetSchoolYearsAsync() =>
        inFactory.GetAllAsync<SchoolYearDataModel, SchoolYear>();
    
    public async Task<Option<SchoolYear>> GetCurrentSchoolYearAsync(DateTime inToday) =>
        Optional(
            (await inFactory.GetAllAsync<SchoolYearDataModel, SchoolYear>(inQueryable => inQueryable
                .Where(inSchoolYear => inSchoolYear.StartDate <= inToday && inSchoolYear.EndDate >= inToday)))
            .SingleOrDefault());
    
    public async Task<Option<SchoolYear>> GetPreviousSchoolYearAsync(Guid inReferenceSchoolYearId) =>
        await (await GetSchoolYearById(inReferenceSchoolYearId))
            .MatchAsync(async inSome =>
                    Optional(
                        (await inFactory.GetAllAsync<SchoolYearDataModel, SchoolYear>(inQueryable => inQueryable
                            .OrderByDescending(inSchoolYear => inSchoolYear.StartDate)
                            .Where(inSchoolYear => inSchoolYear.StartDate < inSome.StartDate)
                        ))
                        .FirstOrDefault()),
                () => Option<SchoolYear>.None);
    
    public async Task<Option<SchoolYear>> GetNextSchoolYearAsync(Guid inReferenceSchoolYearId) =>
        await (await GetSchoolYearById(inReferenceSchoolYearId))
            .MatchAsync(async inSome =>
                    Optional(
                        (await inFactory.GetAllAsync<SchoolYearDataModel, SchoolYear>(inQueryable => inQueryable
                            .OrderBy(inSchoolYear => inSchoolYear.StartDate)
                            .Where(inSchoolYear => inSchoolYear.StartDate > inSome.StartDate)
                        ))
                        .FirstOrDefault()),
                () => Option<SchoolYear>.None);
    
    public Task<bool> AnySchoolYearAsync(Guid inSchoolYearId) =>
        inFactory.AnyAsync<SchoolYearDataModel>(inSchoolYearId);
    
    public Task DeleteSchoolYearAsync(Guid inSchoolYearId) =>
        inFactory.DeleteAsync<SchoolYearDataModel>(inSchoolYearId);
    
    public Task SaveSchoolYearAsync(SchoolYear inSchoolYear) =>
        inFactory.SaveAsync<SchoolYearDataModel, SchoolYear>(inSchoolYear);
    
    public Task<bool> CanSchoolYearBeDeletedAsync(Guid inSchoolYearId) =>
        inFactory.CanBeDeleted<SchoolYearDataModel>(inSchoolYearId);
    
    private async Task<Option<SchoolYear>> GetSchoolYearById(Guid inSchoolYearId) =>
        await inFactory.GetSingleAsync<SchoolYearDataModel, SchoolYear>(inSchoolYearId);
}