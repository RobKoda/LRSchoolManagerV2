// ReSharper disable ClassNeverInstantiated.Global - Implicit use

using LRSchoolV2.Domain.Consultants;

namespace LRSchoolV2.Domain.ConsultantQuotes;

public record ConsultantQuote(
    Guid Id,
    string Number,
    DateTime Date,
    Consultant Consultant,
    string QuoteConsultantName,
    string QuoteConsultantAddress,
    decimal TotalToPay,
    bool EmailSent)
{
    public decimal GetTotalToPayFromItems(IEnumerable<ConsultantQuoteItem> inItems) =>
        inItems
            .Where(inItem => inItem.ConsultantQuote.Id == Id)
            .Sum(inItem => inItem.GetTotal());
    
    public static string GetQuoteNumber(DateTime inDate, IEnumerable<ConsultantQuote> inAllQuotes, Guid inConsultantId) =>
        $"LRS{inDate.Year}-{inAllQuotes.Count(inQuote => inQuote.Date.Year == inDate.Year && inQuote.Consultant.Id == inConsultantId) + 1:D3}";
}
