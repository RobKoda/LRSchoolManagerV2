using LRSchoolV2.Domain.Consultants;

// ReSharper disable NotAccessedPositionalProperty.Global - Implicit use
namespace LRSchoolV2.Domain.ConsultantInvoices;

public record ConsultantInvoiceable(
    ConsultantInvoiceableReferenceType ConsultantInvoiceableReferenceType, 
    Guid ReferenceId, 
    Consultant Consultant, 
    string Denomination, 
    decimal Price, 
    decimal AlreadyBilled,
    int BilledPaymentsCount,
    int PaymentsCount,
    bool CompletePayment);