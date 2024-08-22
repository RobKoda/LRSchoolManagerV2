using LRSchoolV2.Domain.AnnualServices;
using LRSchoolV2.Domain.Common;
using LRSchoolV2.Domain.Consultants;

// ReSharper disable NotAccessedPositionalProperty.Global - Implicit use
namespace LRSchoolV2.Domain.ConsultantInvoices;

public record ConsultantInvoiceable(
    ConsultantInvoiceableReferenceType ConsultantInvoiceableReferenceType, 
    SchoolYear SchoolYear,
    Consultant Consultant,
    AnnualService AnnualService,
    Guid ReferenceId, 
    string Denomination, 
    decimal Price, 
    decimal AlreadyBilled,
    int BilledPaymentsCount,
    int PaymentsCount,
    bool CompletePayment);