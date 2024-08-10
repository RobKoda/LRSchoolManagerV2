namespace LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.CancelCustomerQuote;

public static class CancelCustomerQuoteRequestValidationErrors
{
    public const string NotTheLastCustomerQuote = "Le devis n'est pas le dernier. Annulation impossible";
    public const string CustomerQuoteCannotBeDeleted = "Le devis ne peut pas être annulé car il est lié à d'autres données";
}