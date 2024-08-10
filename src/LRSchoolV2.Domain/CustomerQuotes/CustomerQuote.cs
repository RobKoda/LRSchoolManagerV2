// ReSharper disable ClassNeverInstantiated.Global - Implicit use

using LRSchoolV2.Domain.Persons;

namespace LRSchoolV2.Domain.CustomerQuotes;

public record CustomerQuote(
    Guid Id,
    string Number,
    DateTime Date,
    Person Customer,
    string QuoteCustomerName,
    string QuoteCustomerAddress,
    decimal TotalToPay,
    bool EmailSent)
{
    public decimal GetTotalToPayFromItems(IEnumerable<CustomerQuoteItem> inItems) =>
        inItems
            .Where(inItem => inItem.CustomerQuote.Id == Id)
            .Sum(inItem => inItem.GetTotal());
    
    public static string GetQuoteNumber(DateTime inDate, IEnumerable<CustomerQuote> inAllQuotes) =>
        $"{inDate.Year}-{inAllQuotes.Count(inQuote => inQuote.Date.Year == inDate.Year) + 1:D3}";
}
