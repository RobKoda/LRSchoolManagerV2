// ReSharper disable ClassNeverInstantiated.Global - Implicit use

using LRSchoolV2.Domain.Consultants;

namespace LRSchoolV2.Domain.ConsultantInvoices;

public record ConsultantInvoice(
    Guid Id,
    string Number,
    DateTime Date,
    Consultant Consultant,
    string InvoiceConsultantName,
    string InvoiceConsultantAddress,
    decimal TotalToPay,
    bool EmailSent)
{
    public decimal GetTotalToPayFromItems(IEnumerable<ConsultantInvoiceItem> inItems) =>
        inItems
            .Where(inItem => inItem.ConsultantInvoice.Id == Id)
            .Sum(inItem => inItem.GetTotal());
    
    public static string GetInvoiceNumber(DateTime inDate, IEnumerable<ConsultantInvoice> inAllInvoices, Guid inConsultantId) =>
        $"LRS{inDate.Year}-{inAllInvoices.Count(inInvoice => inInvoice.Date.Year == inDate.Year && inInvoice.Consultant.Id == inConsultantId) + 1:D3}";
}
