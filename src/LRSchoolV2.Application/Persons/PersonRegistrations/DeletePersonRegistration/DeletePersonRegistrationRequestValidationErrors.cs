namespace LRSchoolV2.Application.Persons.PersonRegistrations.DeletePersonRegistration;

public static class DeletePersonRegistrationRequestValidationErrors
{
    public const string PersonRegistrationNotFound = "L'adhésion n'a pas été trouvée";
    public const string PersonRegistrationCannotBeDeleted = "L'adhésion ne peut pas être supprimée car elle est liée à d'autres données";
    public const string PersonRegistrationHasAssociatedAnnualServiceVariations = "L'adhésion ne peut pas être supprimée car la personne est inscrite pour la durée de cette adhésion";
}