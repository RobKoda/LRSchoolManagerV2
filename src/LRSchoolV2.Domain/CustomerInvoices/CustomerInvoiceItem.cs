// ReSharper disable ClassNeverInstantiated.Global - Implicit use
// ReSharper disable NotAccessedPositionalProperty.Global - Implicit use
namespace LRSchoolV2.Domain.CustomerInvoices;

public record CustomerInvoiceItem(
    Guid Id, 
    CustomerInvoice CustomerInvoice, 
    Guid ReferenceId,
    int Quantity, 
    string Denomination, 
    decimal UnitPrice)
{
    public decimal GetTotal() => Quantity * UnitPrice;
}