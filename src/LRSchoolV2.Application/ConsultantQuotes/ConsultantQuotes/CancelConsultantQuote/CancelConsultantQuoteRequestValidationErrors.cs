namespace LRSchoolV2.Application.ConsultantQuotes.ConsultantQuotes.CancelConsultantQuote;

public static class CancelConsultantQuoteRequestValidationErrors
{
    public const string NotTheLastConsultantQuote = "Le devis n'est pas le dernier. Annulation impossible";
    public const string ConsultantQuoteCannotBeDeleted = "Le devis ne peut pas être annulé car il est lié à d'autres données";
}