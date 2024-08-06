// ReSharper disable ClassNeverInstantiated.Global - Implicit use
namespace LRSchoolV2.Domain.Consultants;

public record ConsultantQuote(
    Guid Id,
    string Number,
    DateTime Date,
    Consultant Consultant,
    decimal TotalToPay
)
{
    public static string GetNewConsultantQuoteNumber(int inYear, int inNumber) => $"LR{inYear}-{inNumber:D3}";
    
    public decimal GetTotalToPayFromItems(IEnumerable<ConsultantQuoteItem> inItems) =>
        inItems
            .Where(inItem => inItem.ConsultantQuote.Id == Id)
            .Sum(inItem => inItem.GetTotal());
}