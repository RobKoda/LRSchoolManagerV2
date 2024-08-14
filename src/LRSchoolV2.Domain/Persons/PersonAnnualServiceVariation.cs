// ReSharper disable NotAccessedPositionalProperty.Global - Implicit use

using LRSchoolV2.Domain.AnnualServices;
using LRSchoolV2.Domain.Common;
using LRSchoolV2.Domain.ConsultantInvoices;
using LRSchoolV2.Domain.CustomerInvoices;

namespace LRSchoolV2.Domain.Persons;

public record PersonAnnualServiceVariation(
    Guid Id,
    Person Person,
    SchoolYear SchoolYear,
    AnnualServiceVariation AnnualServiceVariation,
    int PaymentsCount,
    Person? BilledPerson,
    int ConsultantPaymentsCount
)
{
    public static decimal GetAlreadyBilled(IEnumerable<CustomerInvoiceItem> inNonBilledPersonServiceVariationPayments, PersonAnnualServiceVariation inPersonAnnualServiceVariation) => 
        inNonBilledPersonServiceVariationPayments.Where(inPayment => inPayment.ReferenceId == inPersonAnnualServiceVariation.Id).Sum(inPayment => inPayment.GetTotal());

    public static int GetBilledPaymentsCount(IEnumerable<CustomerInvoiceItem> inNonBilledPersonServiceVariationPayments, PersonAnnualServiceVariation inPersonAnnualServiceVariation) => 
        inNonBilledPersonServiceVariationPayments.Count(inPayment => inPayment.ReferenceId == inPersonAnnualServiceVariation.Id);
    
    public static decimal GetConsultantAlreadyBilled(IEnumerable<ConsultantInvoiceItem> inNonBilledPersonServiceVariationPayments, PersonAnnualServiceVariation inPersonAnnualServiceVariation) =>
        inNonBilledPersonServiceVariationPayments.Where(inPayment => inPayment.ReferenceId == inPersonAnnualServiceVariation.Id).Sum(inPayment => inPayment.GetTotal());
    
    public static int GetConsultantBilledPaymentsCount(IEnumerable<ConsultantInvoiceItem> inNonBilledPersonServiceVariationPayments, PersonAnnualServiceVariation inPersonAnnualServiceVariation) =>
        inNonBilledPersonServiceVariationPayments.Count(inPayment => inPayment.ReferenceId == inPersonAnnualServiceVariation.Id);
}