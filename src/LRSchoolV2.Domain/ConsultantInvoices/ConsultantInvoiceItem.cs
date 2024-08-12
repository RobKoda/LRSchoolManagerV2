// ReSharper disable ClassNeverInstantiated.Global - Implicit use
// ReSharper disable NotAccessedPositionalProperty.Global - Implicit use

using LanguageExt;

namespace LRSchoolV2.Domain.ConsultantInvoices;

public record ConsultantInvoiceItem(
    Guid Id, 
    ConsultantInvoice ConsultantInvoice,
    Option<Guid> ReferenceId,
    int Quantity, 
    string Denomination, 
    decimal UnitPrice,
    int Order)
{
    public decimal GetTotal() => Quantity * UnitPrice;
}