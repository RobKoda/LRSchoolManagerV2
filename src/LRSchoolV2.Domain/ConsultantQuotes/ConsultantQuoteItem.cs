// ReSharper disable ClassNeverInstantiated.Global - Implicit use
// ReSharper disable NotAccessedPositionalProperty.Global - Implicit use
namespace LRSchoolV2.Domain.ConsultantQuotes;

public record ConsultantQuoteItem(
    Guid Id, 
    ConsultantQuote ConsultantQuote, 
    int Quantity, 
    string Denomination, 
    decimal UnitPrice,
    int Order)
{
    public decimal GetTotal() => Quantity * UnitPrice;
}