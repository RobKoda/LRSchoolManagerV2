namespace LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.SaveConsultantInvoice;

public static class SaveConsultantInvoiceRequestValidationErrors
{
    public const string ConsultantNotFound = "L'intervenant {personName} n'a pas été trouvé";
    public const string ConsultantHasNoAddress = "L'intervenant {personName} n'a pas d'adresse valide";
    public const string NumberInvalidFormat = "Le numéro de facture {number} est invalide";
}