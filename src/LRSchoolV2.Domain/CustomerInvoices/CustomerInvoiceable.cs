// ReSharper disable NotAccessedPositionalProperty.Global - Implicit use

using LRSchoolV2.Domain.Persons;

namespace LRSchoolV2.Domain.CustomerInvoices;

public record CustomerInvoiceable(
    CustomerInvoiceableReferenceType CustomerInvoiceableReferenceType, 
    Guid ReferenceId, 
    Person Person, 
    string Denomination, 
    decimal Price, 
    decimal AlreadyBilled,
    int BilledPaymentsCount,
    int PaymentsCount,
    bool CompletePayment);