// ReSharper disable ClassNeverInstantiated.Global - Implicit use
// ReSharper disable NotAccessedPositionalProperty.Global - Implicit use
namespace LRSchoolV2.Domain.Consultants;

public record ConsultantQuoteItem(
    Guid Id,
    ConsultantQuote ConsultantQuote,
    int Quantity,
    string Denomination,
    decimal UnitPrice)
{
    public decimal GetTotal() => UnitPrice * Quantity;
}