using LRSchoolV2.Domain.CustomerInvoices;
using LRSchoolV2.Domain.Persons;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use
// ReSharper disable UnusedMember.Global - Implicit use
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global - Implicit use

namespace LRSchoolV2.Blazor.Pages.CustomerInvoices.CustomerInvoicesGeneration;
    
public class EditablePayable
{
    public PayableReferenceType PayableReferenceType { get; set; } = null!;
    
    public Guid ReferenceId { get; set; }

    public Person Person { get; set; } = null!;

    public string Denomination { get; set; } = string.Empty;
    
    public decimal Price { get; set; }
    
    public decimal AlreadyBilled { get; set; }
    
    public int BilledPaymentsCount { get; set; }
    
    public int PaymentsCount { get; set; }
    
    public bool CompletePayment { get; set; }

    public decimal GetLeftToBill() => Price - AlreadyBilled;
}