using LanguageExt;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use
// ReSharper disable NotAccessedPositionalProperty.Global - Implicit use
namespace LRSchoolV2.Domain.CustomerInvoices;

public record CustomerInvoiceItem(
    Guid Id, 
    CustomerInvoice CustomerInvoice, 
    Option<Guid> ReferenceId,
    int Quantity, 
    string Denomination, 
    decimal UnitPrice,
    int Order)
{
    public decimal GetTotal() => Quantity * UnitPrice;
}