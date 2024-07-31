using LanguageExt;
using LRSchoolV2.Domain.Common;

namespace LRSchoolV2.Application.Common.SchoolYears.GetCurrentSchoolYear;

public record GetCurrentSchoolYearResponse(Option<SchoolYear> SchoolYear);