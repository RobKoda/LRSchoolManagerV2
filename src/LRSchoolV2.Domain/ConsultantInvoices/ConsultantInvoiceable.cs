// ReSharper disable NotAccessedPositionalProperty.Global - Implicit use

using LRSchoolV2.Domain.Common;
using LRSchoolV2.Domain.Consultants;

namespace LRSchoolV2.Domain.ConsultantInvoices;

public record ConsultantInvoiceable(
    ConsultantInvoiceableReferenceType ConsultantInvoiceableReferenceType, 
    SchoolYear SchoolYear,
    Consultant Consultant,
    Guid ReferenceId, 
    string Denomination, 
    decimal Price, 
    decimal AlreadyBilled,
    int BilledPaymentsCount,
    int PaymentsCount,
    bool CompletePayment);