using LRSchoolV2.Domain.AnnualServices;
using LRSchoolV2.Domain.Common;
using LRSchoolV2.Domain.ConsultantInvoices;
using LRSchoolV2.Domain.Consultants;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global - Implicit use
// ReSharper disable ClassNeverInstantiated.Global - Implicit use
// ReSharper disable UnusedMember.Global - Implicit use
namespace LRSchoolV2.Blazor.Pages.ConsultantInvoices.ConsultantInvoicesGeneration;
    
public class ConsultantInvoiceableFormModel
{
    public ConsultantInvoiceableReferenceType ConsultantInvoiceableReferenceType { get; set; } = null!;
    
    public SchoolYear SchoolYear { get; set; } = null!;
    
    public Consultant Consultant { get; set; } = null!;
    
    public AnnualService AnnualService { get; set; } = null!;
    
    public Guid ReferenceId { get; set; }

    public string Denomination { get; set; } = string.Empty;
    
    public decimal Price { get; set; }
    
    public decimal AlreadyBilled { get; set; }
    
    public int BilledPaymentsCount { get; set; }
    
    public int PaymentsCount { get; set; }
    
    public bool CompletePayment { get; set; }

    public decimal GetLeftToBill() => Price - AlreadyBilled;
}