namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariations.DeleteAnnualServiceVariation;

public static class DeleteAnnualServiceVariationRequestValidationErrors
{
    public const string AnnualServiceVariationNotFound = "La variation n'a pas été trouvé";
    public const string AnnualServiceVariationCannotBeDeleted = "La variation ne peut pas être supprimée car elle est liée à d'autres données";
}