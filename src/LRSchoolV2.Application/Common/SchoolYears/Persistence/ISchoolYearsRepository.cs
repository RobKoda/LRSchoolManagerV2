using LanguageExt;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Domain.Common;

namespace LRSchoolV2.Application.Common.SchoolYears.Persistence;

public interface ISchoolYearsRepository : IRepository
{
    Task<IEnumerable<SchoolYear>> GetSchoolYearsAsync();
    Task SaveSchoolYearAsync(SchoolYear inSchoolYear);
    Task DeleteSchoolYearAsync(Guid inSchoolYearId);
    Task<bool> AnySchoolYearAsync(Guid inSchoolYearId);
    Task<Option<SchoolYear>> GetCurrentSchoolYearAsync(DateTime inToday);
    Task<Option<SchoolYear>> GetPreviousSchoolYearAsync(Guid inReferenceSchoolYearId);
    Task<Option<SchoolYear>> GetNextSchoolYearAsync(Guid inReferenceSchoolYearId);
    Task<bool> CanSchoolYearBeDeletedAsync(Guid inSchoolYearId);
}