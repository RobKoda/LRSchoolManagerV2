namespace LRSchoolV2.Application.Common.SchoolYears.SaveSchoolYear;

public static class SaveSchoolYearRequestValidationErrors
{
    public const string SchoolYearStartDateNotRightAfterPreviousEndDate = "L'année scolaire doit suivre la précédente";
    public const string SchoolYearEndDateNotRightBeforeNextStartDate = "L'année scolaire doit précéder la suivante";
}