// ReSharper disable ClassNeverInstantiated.Global - Implicit use
// ReSharper disable NotAccessedPositionalProperty.Global - Implicit use

using LanguageExt;
using LRSchoolV2.Domain.Common;

namespace LRSchoolV2.Domain.ConsultantInvoices;

public record ConsultantInvoiceItem(
    Guid Id, 
    ConsultantInvoice ConsultantInvoice,
    SchoolYear SchoolYear,
    Option<Guid> ReferenceId,
    int Quantity, 
    string Denomination, 
    decimal UnitPrice,
    int Order)
{
    public decimal GetTotal() => Quantity * UnitPrice;
}