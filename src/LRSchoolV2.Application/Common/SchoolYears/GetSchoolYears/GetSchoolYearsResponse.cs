using LRSchoolV2.Domain.Common;

namespace LRSchoolV2.Application.Common.SchoolYears.GetSchoolYears;

public record GetSchoolYearsResponse(IEnumerable<SchoolYear> SchoolYears);
