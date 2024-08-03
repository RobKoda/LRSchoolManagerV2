namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariationConsultantWorks.DeleteAnnualServiceVariationConsultantWork;

public static class DeleteAnnualServiceVariationConsultantWorkRequestValidationErrors
{
    public const string AnnualServiceVariationConsultantWorkNotFound = "L'horaire d'intervenant n'a pas été trouvée";
    public const string AnnualServiceVariationConsultantWorkCannotBeDeleted = "L'horaire d'intervenant ne peut pas être supprimée car elle est liée à d'autres données";
}