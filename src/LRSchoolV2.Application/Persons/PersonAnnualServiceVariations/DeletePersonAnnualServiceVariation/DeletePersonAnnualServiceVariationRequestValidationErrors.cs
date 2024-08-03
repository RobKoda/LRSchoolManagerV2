namespace LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.DeletePersonAnnualServiceVariation;

public static class DeletePersonAnnualServiceVariationRequestValidationErrors
{
    public const string PersonAnnualServiceVariationNotFound = "L'inscription n'a pas été trouvée";
    public const string PersonAnnualServiceVariationCannotBeDeleted = "L'inscription ne peut pas être supprimée car elle est liée à d'autres données";
}