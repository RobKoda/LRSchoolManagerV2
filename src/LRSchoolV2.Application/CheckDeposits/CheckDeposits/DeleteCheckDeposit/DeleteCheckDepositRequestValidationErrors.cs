namespace LRSchoolV2.Application.CheckDeposits.CheckDeposits.DeleteCheckDeposit;

public static class DeleteCheckDepositRequestValidationErrors
{
    public const string CheckDepositNotFound = "La remise de chèque n'a pas été trouvée";
    public const string CheckDepositCannotBeDeleted = "La remise de chèque ne peut pas être supprimée car elle est liée à d'autres données";
}