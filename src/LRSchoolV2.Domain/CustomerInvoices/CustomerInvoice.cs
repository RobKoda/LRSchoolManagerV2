// ReSharper disable ClassNeverInstantiated.Global - Implicit use

using LRSchoolV2.Domain.Persons;

namespace LRSchoolV2.Domain.CustomerInvoices;

public record CustomerInvoice(
    Guid Id,
    string Number,
    DateTime Date,
    Person Customer,
    string InvoiceCustomerName,
    string InvoiceCustomerAddress,
    decimal TotalToPay,
    bool EmailSent)
{
    public decimal GetTotalToPayFromItems(IEnumerable<CustomerInvoiceItem> inItems) =>
        inItems
            .Where(inItem => inItem.CustomerInvoice.Id == Id)
            .Sum(inItem => inItem.GetTotal());
    
    public static string GetInvoiceNumber(DateTime inDate, IEnumerable<CustomerInvoice> inAllInvoices) =>
        $"{inDate.Year}-{inAllInvoices.Count(inInvoice => inInvoice.Date.Year == inDate.Year) + 1:D3}";
}
