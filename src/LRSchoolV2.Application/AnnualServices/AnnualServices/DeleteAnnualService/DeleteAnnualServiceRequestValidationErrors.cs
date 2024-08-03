namespace LRSchoolV2.Application.AnnualServices.AnnualServices.DeleteAnnualService;

public static class DeleteAnnualServiceRequestValidationErrors
{
    public const string AnnualServiceNotFound = "Le service annuel n'a pas été trouvé";
    public const string AnnualServiceCannotBeDeleted = "Le service annuel ne peut pas être supprimé car il est liée à d'autres données";
}