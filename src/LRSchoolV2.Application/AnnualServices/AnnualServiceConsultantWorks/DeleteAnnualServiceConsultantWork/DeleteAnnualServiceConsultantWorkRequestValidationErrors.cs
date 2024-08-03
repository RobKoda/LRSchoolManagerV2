namespace LRSchoolV2.Application.AnnualServices.AnnualServiceConsultantWorks.DeleteAnnualServiceConsultantWork;

public static class DeleteAnnualServiceConsultantWorkRequestValidationErrors
{
    public const string AnnualServiceConsultantWorkNotFound = "L'horaire d'intervenant n'a pas été trouvée";
    public const string AnnualServiceConsultantWorkCannotBeDeleted = "L'horaire d'intervenant ne peut pas être supprimée car elle est liée à d'autres données";
}