namespace LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.CancelCustomerInvoice;

public static class CancelCustomerInvoiceRequestValidationErrors
{
    public const string NotTheLastCustomerInvoice = "La facture n'est pas la dernière. Annulation impossible";
    public const string CustomerInvoiceCannotBeDeleted = "La facture ne peut pas être annulée car elle est liée à d'autres données";
}