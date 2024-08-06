namespace LRSchoolV2.Application.CustomerPayments.CustomerPayments.SaveCustomerPayment;

public static class SaveCustomerPaymentRequestValidationErrors
{
    public const string InvalidAmount = "Le montant est invalide";
    public const string NonCheckIsReferencedInCustomerPayment = "Le paiement fait parti d'une remise de chèque. Ce paiement ne peut pas être autre chose qu'un chèque";
}