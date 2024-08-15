using LRSchoolV2.Domain.Common;
using LRSchoolV2.Domain.ConsultantInvoices;
using LRSchoolV2.Domain.Consultants;

// ReSharper disable NotAccessedPositionalProperty.Global - Implicit use
// ReSharper disable UnusedMember.Global - Implicit use
namespace LRSchoolV2.Domain.AnnualServices;

public record AnnualServiceVariation(
    Guid Id,
    AnnualService AnnualService,
    string Name,
    string InvoiceName,
    AnnualServiceVariationYearlyPrice? CurrentYearlyPrice)
{
    public string GetFullName() => $"{AnnualService.Name} {Name}";
    public decimal GetPrice() => CurrentYearlyPrice?.Price ?? 0m;
    public decimal GetMargin() => CurrentYearlyPrice?.Margin ?? 0m;
    
    public static decimal GetConsultantAlreadyBilled(IEnumerable<ConsultantInvoiceItem> inConsultantInvoiceItems, AnnualServiceVariation inAnnualServiceVariation, Consultant inConsultant, SchoolYear inSchoolYear) =>
        inConsultantInvoiceItems
            .Where(inInvoiceItem => inInvoiceItem.ReferenceId == inAnnualServiceVariation.Id && 
                                                  inInvoiceItem.ConsultantInvoice.Consultant.Id == inConsultant.Id &&
                                                  inInvoiceItem.SchoolYear.Id == inSchoolYear.Id)
            .Sum(inPayment => inPayment.GetTotal());
    
    public static int GetConsultantBilledPaymentsCount(IEnumerable<ConsultantInvoiceItem> inNonBilledPersonServiceVariationPayments, AnnualServiceVariation inAnnualServiceVariation, Consultant inConsultant, SchoolYear inSchoolYear) =>
        inNonBilledPersonServiceVariationPayments
            .Count(inInvoiceItem => inInvoiceItem.ReferenceId == inAnnualServiceVariation.Id);
}