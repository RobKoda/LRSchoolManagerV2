namespace LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.SaveCustomerQuote;

public static class SaveCustomerQuoteRequestValidationErrors
{
    public const string CustomerNotFound = "Le client {personName} n'a pas été trouvé";
    public const string CustomerHasNoAddress = "Le client {personName} n'a pas d'adresse valide";
    public const string NumberInvalidFormat = "Le numéro de devis {number} est invalide";
}