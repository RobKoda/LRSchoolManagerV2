namespace LRSchoolV2.Application.Consultants.Consultants.DeleteConsultant;

public static class DeleteConsultantRequestValidationErrors
{
    public const string ConsultantNotFound = "L'intervenant n'a pas été trouvé";
    public const string ConsultantCannotBeDeleted = "L'intervenant ne peut pas être supprimé car il est liée à d'autres données";
}