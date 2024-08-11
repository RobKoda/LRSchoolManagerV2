namespace LRSchoolV2.Application.ConsultantQuotes.ConsultantQuotes.SaveConsultantQuote;

public static class SaveConsultantQuoteRequestValidationErrors
{
    public const string ConsultantNotFound = "L'intervenant {personName} n'a pas été trouvé";
    public const string ConsultantHasNoAddress = "L'intervenant {personName} n'a pas d'adresse valide";
    public const string NumberInvalidFormat = "Le numéro de devis {number} est invalide";
}