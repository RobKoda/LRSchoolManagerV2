namespace LRSchoolV2.Application.CheckDeposits.CheckDepositPayments.DeleteCheckDepositPayment;

public static class DeleteCheckDepositPaymentRequestValidationErrors
{
    public const string CheckDepositPaymentNotFound = "Le chèque n'a pas été trouvé";
    public const string CheckDepositPaymentCannotBeDeleted = "Le chèque ne peut pas être supprimé car il est lié à d'autres données";
}