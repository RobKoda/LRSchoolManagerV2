namespace LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.CancelConsultantInvoice;

public static class CancelConsultantInvoiceRequestValidationErrors
{
    public const string NotTheLastConsultantInvoice = "La facture n'est pas le dernier. Annulation impossible";
    public const string ConsultantInvoiceCannotBeDeleted = "La facture ne peut pas être annulée car elle est liée à d'autres données";
}