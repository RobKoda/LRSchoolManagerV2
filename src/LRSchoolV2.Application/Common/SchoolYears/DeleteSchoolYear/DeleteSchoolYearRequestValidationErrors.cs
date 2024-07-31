namespace LRSchoolV2.Application.Common.SchoolYears.DeleteSchoolYear;

public static class DeleteSchoolYearRequestValidationErrors
{
    public const string SchoolYearNotFound = "L'année scolaire n'a pas été trouvée";
    public const string SchoolYearBetweenTwoSchoolYears = "L'année scolaire se trouve entre 2 autres";
    public const string SchoolYearCannotBeDeleted = "L'année scolaire ne peut pas être supprimée car elle est liée à d'autres données";
}