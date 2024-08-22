// ReSharper disable NotAccessedPositionalProperty.Global - Implicit use
// ReSharper disable UnusedMember.Global - Implicit use

using LRSchoolV2.Domain.Common;
using LRSchoolV2.Domain.ConsultantInvoices;
using LRSchoolV2.Domain.Consultants;

namespace LRSchoolV2.Domain.AnnualServices;

public record AnnualService(
    Guid Id,
    string Name)
{
    public static decimal GetConsultantAlreadyBilled(IEnumerable<ConsultantInvoiceItem> inConsultantInvoiceItems, AnnualService inAnnualService, Consultant inConsultant, SchoolYear inSchoolYear) =>
        inConsultantInvoiceItems
            .Where(inInvoiceItem => inInvoiceItem.ReferenceId == inAnnualService.Id &&
                                    inInvoiceItem.ConsultantInvoice.Consultant.Id == inConsultant.Id &&
                                    inInvoiceItem.SchoolYear.Id == inSchoolYear.Id)
            .Sum(inPayment => inPayment.GetTotal());
    
    public static int GetConsultantBilledPaymentsCount(IEnumerable<ConsultantInvoiceItem> inNonBilledPersonServiceVariationPayments, AnnualService inAnnualService, Consultant inConsultant, SchoolYear inSchoolYear) =>
        inNonBilledPersonServiceVariationPayments
            .Count(inInvoiceItem => inInvoiceItem.ReferenceId == inAnnualService.Id &&
                                    inInvoiceItem.ConsultantInvoice.Consultant.Id == inConsultant.Id &&
                                    inInvoiceItem.SchoolYear.Id == inSchoolYear.Id);
}
