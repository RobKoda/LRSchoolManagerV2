namespace LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.SaveCustomerInvoice;

public static class SaveCustomerInvoiceRequestValidationErrors
{
    public const string CustomerNotFound = "Le client {personName} n'a pas été trouvé";
    public const string CustomerHasNoAddress = "Le client {personName} n'a pas d'adresse valide";
    public const string NumberInvalidFormat = "Le numéro de facture {number} est invalide";
}