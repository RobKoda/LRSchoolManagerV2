using LanguageExt;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use
// ReSharper disable NotAccessedPositionalProperty.Global - Implicit use
namespace LRSchoolV2.Domain.CustomerQuotes;

public record CustomerQuoteItem(
    Guid Id, 
    CustomerQuote CustomerQuote, 
    Option<Guid> ReferenceId,
    int Quantity, 
    string Denomination, 
    decimal UnitPrice)
{
    public decimal GetTotal() => Quantity * UnitPrice;
}