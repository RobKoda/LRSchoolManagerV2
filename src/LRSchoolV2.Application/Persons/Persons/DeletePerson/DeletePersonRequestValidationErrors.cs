namespace LRSchoolV2.Application.Persons.Persons.DeletePerson;

public static class DeletePersonRequestValidationErrors
{
    public const string PersonNotFound = "L'intervenant n'a pas été trouvé";
    public const string PersonCannotBeDeleted = "La personne ne peut être supprimée car elle est liée à d'autres données";
}