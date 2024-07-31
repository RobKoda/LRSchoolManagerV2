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
    Task<Option<SchoolYear>> GetCurrentSchoolYearAsync();
    Task<Option<SchoolYear>> GetPreviousSchoolYearAsync(SchoolYear inReferenceSchoolYear);
    Task<Option<SchoolYear>> GetNextSchoolYearAsync(SchoolYear inReferenceSchoolYear);
    Task<bool> CanSchoolYearBeDeleted(Guid inSchoolYearId);
}