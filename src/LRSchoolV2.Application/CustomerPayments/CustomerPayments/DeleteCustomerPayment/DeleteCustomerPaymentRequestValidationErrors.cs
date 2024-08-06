namespace LRSchoolV2.Application.CustomerPayments.CustomerPayments.DeleteCustomerPayment;

public static class DeleteCustomerPaymentRequestValidationErrors
{
    public const string CustomerPaymentNotFound = "Le paiement client n'a pas été trouvée";
    public const string CustomerPaymentCannotBeDeleted = "Le paiement client ne peut pas être supprimé car il est lié à d'autres données";
}